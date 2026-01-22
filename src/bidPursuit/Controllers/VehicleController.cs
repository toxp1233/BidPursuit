using bidPursuit.Application.Vehicles.Commands.AssignVehicleToAuction;
using bidPursuit.Application.Vehicles.Commands.CreateVehicle;
using bidPursuit.Application.Vehicles.Commands.DeleteVehicle;
using bidPursuit.Application.Vehicles.Commands.UpdateVehicle;
using bidPursuit.Application.Vehicles.Querys.GetAllVehicles;
using bidPursuit.Application.Vehicles.Querys.GetAllVehiclesById;
using bidPursuit.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace bidPursuit.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VehicleController(IMediator mediator) : ControllerBase
{
    // PUBLIC — anyone can view vehicles
    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetVehicles([FromQuery] GetAllVehiclesQuery query)
    {
        var result = await mediator.Send(query);
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetVehicleById(Guid id)
    {
        var result = await mediator.Send(new GetVehicleByIdQuery(id));
        return Ok(result);
    }

    // AGENT / ADMIN — manage vehicles
    [Authorize(Roles = $"{nameof(Roles.Agent)},{nameof(Roles.Admin)}")]
    [HttpPost]
    public async Task<IActionResult> CreateVehicle(CreateVehicleCommand command)
    {
        command.UserId = GetUserId();
        await mediator.Send(command);
        return NoContent();
    }

    [Authorize(Roles = $"{nameof(Roles.Agent)},{nameof(Roles.Admin)}")]
    [HttpPut("{vehicleId:guid}/auction/{auctionId:guid}")]
    public async Task<IActionResult> AssignVehicleToAuction(Guid vehicleId, Guid auctionId)
    {
        await mediator.Send(new AssignVehicleToAuctionCommand(vehicleId, auctionId));
        return NoContent();
    }

    [Authorize(Roles = $"{nameof(Roles.Agent)},{nameof(Roles.Admin)}")]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateVehicle(Guid id, UpdateVehicleCommand command)
    {
        command.Id = id;
        await mediator.Send(command);
        return NoContent();
    }

    [Authorize(Roles = $"{nameof(Roles.Agent)},{nameof(Roles.Admin)}")]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteVehicle(Guid id)
    {
        await mediator.Send(new DeleteVehicleCommand(id));
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
