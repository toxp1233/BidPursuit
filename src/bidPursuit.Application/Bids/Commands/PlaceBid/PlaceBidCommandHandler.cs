using AutoMapper;
using bidPursuit.Application.PublicDtos;
using bidPursuit.Domain.Entities;
using bidPursuit.Domain.Exceptions;
using bidPursuit.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace bidPursuit.Application.Bids.Commands.PlaceBid;

public class PlaceBidCommandHandler(
    IBidRepository bidRepository,
    IVehicleRepository vehicleRepository,
    IAuctionRepository auctionRepository,
    IUserRepository userRepository,
    IAuctionParticipantRepository auctionParticipantRepository,
    IMapper mapper,
    IUnitOfWork unitOfWork
) : IRequestHandler<PlaceBidCommand, VehicleDto>
{
    public async Task<VehicleDto> Handle(PlaceBidCommand request, CancellationToken cancellationToken)
    {
        await unitOfWork.BeginTransactionAsync(cancellationToken);

        try
        {
            // 1. Load vehicle
            var vehicle = await vehicleRepository.GetByIdAsync(request.VehicleId, cancellationToken)
                ?? throw new NotFoundException(nameof(Vehicle), request.VehicleId.ToString());

            if (vehicle.IsSold)
                throw new InvalidBusinessOperationException("Cannot place a bid on a sold vehicle.");

            // 2. Load auction
            var auction = await auctionRepository.GetByIdAsync((Guid)vehicle.AuctionId!, cancellationToken)
                ?? throw new NotFoundException(nameof(Auction), vehicle.AuctionId.ToString()!);

            var isAuctionLive = auction.CurrentCarId == vehicle.Id && auction.IsActive;

            // 3. Early bid phase
            if (!isAuctionLive && !vehicle.EarlyBiddingEnabled)
                throw new InvalidBusinessOperationException("Early bidding is not enabled for this vehicle.");

            // Get highest bid for this vehicle
            var highestBid = await bidRepository.GetHighestBidForVehicleAsync(vehicle.Id, cancellationToken);

            // Prevent self-outbidding
            if (highestBid != null && highestBid.UserId == request.UserId)
                throw new InvalidBusinessOperationException("You cannot outbid yourself. Wait for another user to bid first.");

            // Determine minimum allowed bid
            var minAllowed = highestBid?.Amount ?? vehicle.StartingPrice;

            // Enforce increment rules
            if (request.Increment < 25 || request.Increment > 500 || request.Increment % 25 != 0)
                throw new InvalidBusinessOperationException("Bid increment must be between 25–500 and multiples of 25.");

            // Calculate final bid amount
            var finalBidAmount = minAllowed + request.Increment;

            // 6. Ensure user is a participant
            var participant = await auctionParticipantRepository
                .GetByAuctionAndUserAsync(auction.Id, request.UserId, cancellationToken);

            if (participant == null)
            {
                participant = new AuctionParticipant
                {
                    Id = Guid.NewGuid(),
                    AuctionId = auction.Id,
                    UserId = request.UserId,
                    IsActive = true,
                    HasBidded = true,
                    IsEarlyBidder = !isAuctionLive,
                    JoinedAt = DateTime.UtcNow
                };

                await auctionParticipantRepository.AddAsync(participant, cancellationToken);
                auction.Participants.Add(participant);
            }
            else
            {
                // Update flags if necessary
                participant.HasBidded = true;
                if (!participant.IsEarlyBidder && !isAuctionLive)
                    participant.IsEarlyBidder = true;
            }

            // 7. Ensure user references participant
            var user = await userRepository.GetByIdAsync(request.UserId, cancellationToken)
                ?? throw new NotFoundException(nameof(User), request.UserId.ToString());

            user.AuctionParticipations ??= [];
            if (!user.AuctionParticipations.Contains(participant))
                user.AuctionParticipations.Add(participant);

            // 8. Create bid and add to vehicle collection
            var bid = mapper.Map<Bid>(request);
            bid.AuctionId = auction.Id;
            bid.Amount = finalBidAmount;
            bid.CreatedAt = DateTime.UtcNow;

            await bidRepository.AddAsync(bid, cancellationToken);

            // 9. Save changes and commit transaction
            await unitOfWork.SaveChangesAsync(cancellationToken);
            await unitOfWork.CommitTransactionAsync(cancellationToken);
            var updatedVehicle = await vehicleRepository.GetVehicleById(request.VehicleId, cancellationToken)
                   ?? throw new NotFoundException(nameof(Vehicle), request.VehicleId.ToString());

            // 10. Return vehicle with updated bids
            return mapper.Map<VehicleDto>(updatedVehicle);
        }
        catch (DbUpdateConcurrencyException)
        {

            await unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw new ConcurrencyException("Another bid was placed before yours. Please refresh and try again.");
        }
        catch
        {
            await unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }
}
