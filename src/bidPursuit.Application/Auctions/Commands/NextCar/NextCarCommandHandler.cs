using AutoMapper;
using bidPursuit.Application.PublicDtos;
using bidPursuit.Domain.Entities;
using bidPursuit.Domain.Exceptions;
using bidPursuit.Domain.Interfaces;
using MediatR;

namespace bidPursuit.Application.Auctions.Commands.NextCar;

public class NextCarCommandHandler(
    IAuctionRepository auctionRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper
) : IRequestHandler<NextCarCommand, VehicleDto>
{
    public async Task<VehicleDto> Handle(
        NextCarCommand request,
        CancellationToken cancellationToken)
    {
        var auction = await auctionRepository.GetByIdAsync(
            request.AuctionId,
            cancellationToken)
            ?? throw new NotFoundException(nameof(Auction), request.AuctionId.ToString());

        Vehicle nextVehicle;

        try
        {
            nextVehicle = auction.MoveToNextCar();
        }
        catch (InvalidOperationException ex)
        {
            throw new Exception(ex.Message);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return mapper.Map<VehicleDto>(nextVehicle);
    }
}
