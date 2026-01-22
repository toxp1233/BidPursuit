using AutoMapper;
using bidPursuit.Application.PublicDtos;
using bidPursuit.Domain.Entities;
using bidPursuit.Domain.Exceptions;
using bidPursuit.Domain.Interfaces;
using MediatR;

namespace bidPursuit.Application.Auctions.Commands.JoinAuction;

public class JoinAuctionCommandHandler(
    IUserRepository userRepository,
    IAuctionRepository auctionRepository,
    IAuctionParticipantRepository auctionParticipantRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper
    ) : IRequestHandler<JoinAuctionCommand, AuctionDto>
{
    public async Task<AuctionDto> Handle(JoinAuctionCommand request, CancellationToken cancellationToken)
    {
        var auction = await auctionRepository.GetAuctionById(request.AuctionId, cancellationToken) ?? throw new NotFoundException(nameof(Auction), request.AuctionId.ToString());
        var user = await userRepository.GetByIdAsync(request.UserId, cancellationToken) ?? throw new NotFoundException(nameof(User), request.UserId.ToString());

        if (auction.Participants.Any(p => p.UserId == request.UserId))
        {
            return mapper.Map<AuctionDto>(auction);
        }

        var participant = new AuctionParticipant
        {
            AuctionId = auction.Id,
            UserId = user.Id
        };
        user.AuctionParticipations!.Add(participant);
        await auctionParticipantRepository.AddAsync(participant, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return mapper.Map<AuctionDto>(auction);
    }
}
