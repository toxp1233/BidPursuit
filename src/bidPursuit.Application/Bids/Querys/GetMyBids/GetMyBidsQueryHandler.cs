using AutoMapper;
using bidPursuit.Application.PublicDtos;
using bidPursuit.Domain.Entities;
using bidPursuit.Domain.Exceptions;
using bidPursuit.Domain.Interfaces;
using MediatR;

namespace bidPursuit.Application.Bids.Querys.GetMyBids;

public class GetMyBidsQueryHandler(
    IUserRepository userRepository,
    IMapper mapper
    ) : IRequestHandler<GetMyBidsQuery, List<BidDto>>
{
    public async Task<List<BidDto>> Handle(GetMyBidsQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.UserId, cancellationToken) ?? throw new NotFoundException(nameof(User), request.UserId.ToString());
        return mapper.Map<List<BidDto>>(user.Bids);
    }
}
