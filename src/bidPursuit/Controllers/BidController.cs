using bidPursuit.Application.Bids.Commands.PlaceBid;
using bidPursuit.Application.Bids.Querys.GetMyBids;
using bidPursuit.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace bidPursuit.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = nameof(Roles.User))]
public class BidController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> PlaceBid(PlaceBidCommand command)
    {
        command.UserId = GetUserId();
        var bidId = await mediator.Send(command);
        return Ok(bidId);
    }

    [HttpGet("my")]
    public async Task<IActionResult> GetMyBids()
    {
        var result = await mediator.Send(new GetMyBidsQuery(GetUserId()));
        return Ok(result);
    }

    private Guid GetUserId()
    {
        var claim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (claim == null || !Guid.TryParse(claim.Value, out var userId))
            throw new UnauthorizedAccessException("User ID claim missing or invalid.");

        return userId;
    }
}
