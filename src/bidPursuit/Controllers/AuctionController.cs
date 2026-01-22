using bidPursuit.Application.Auctions.Commands.CloseAuction;
using bidPursuit.Application.Auctions.Commands.CreateAuction;
using bidPursuit.Application.Auctions.Commands.JoinAuction;
using bidPursuit.Application.Auctions.Commands.NextCar;
using bidPursuit.Application.Auctions.Commands.UpdateAuction;
using bidPursuit.Application.Auctions.Querys.GetAllAuctions;
using bidPursuit.Application.Auctions.Querys.GetAuctionById;
using bidPursuit.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace bidPursuit.API.Controllers;

[ApiController]
[Route("api/auctions")]
public class AuctionsController(IMediator mediator) : ControllerBase
{
    // PUBLIC — view auctions
    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetAuctions([FromQuery] GetAllAuctionsQuery query)
    {
        var result = await mediator.Send(query);
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpGet("{auctionId:guid}")]
    public async Task<IActionResult> GetAuctionById(Guid auctionId)
    {
        var result = await mediator.Send(new GetAuctionByIdQuery(auctionId));
        return Ok(result);
    }

    // USER — join auction
    [Authorize(Roles = nameof(Roles.User))]
    [HttpPost("{auctionId:guid}/join")]
    public async Task<IActionResult> JoinAuction(Guid auctionId)
    {
        var result = await mediator.Send(new JoinAuctionCommand(auctionId, GetUserId()));
        return CreatedAtAction(nameof(GetAuctionById), new { auctionId = result.Id }, result);
    }

    // AGENT / ADMIN — manage auction
    [Authorize(Roles = $"{nameof(Roles.Agent)},{nameof(Roles.Admin)}")]
    [HttpPost]
    public async Task<IActionResult> CreateAuction(CreateAuctionCommand command)
    {
        command.OrganizerId = GetUserId();
        var result = await mediator.Send(command);
        return Ok(result);
    }

    [Authorize(Roles = $"{nameof(Roles.Agent)},{nameof(Roles.Admin)}")]
    [HttpPut("{auctionId:guid}")]
    public async Task<IActionResult> UpdateAuction(Guid auctionId, UpdateAuctionCommand command)
    {
        command.Id = auctionId;
        await mediator.Send(command);
        return NoContent();
    }

    [Authorize(Roles = $"{nameof(Roles.Agent)},{nameof(Roles.Admin)}")]
    [HttpPatch("{auctionId:guid}/next-car")]
    public async Task<IActionResult> NextCarInAuction(Guid auctionId)
    {
        var result = await mediator.Send(new NextCarCommand(auctionId));
        return Ok(result);
    }

    [Authorize(Roles = $"{nameof(Roles.Agent)},{nameof(Roles.Admin)}")]
    [HttpPut("{auctionId:guid}/close")]
    public async Task<IActionResult> CloseAuction(Guid auctionId)
    {
        await mediator.Send(new CloseAuctionCommand(auctionId));
        return NoContent();
    }

    private Guid GetUserId()
    {
        var claim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (claim == null || !Guid.TryParse(claim.Value, out var userId))
            throw new UnauthorizedAccessException("User ID claim missing or invalid.");

        return userId;
    }
}
