using bidPursuit.Application.Auctions.Commands.CreateAuction;
using bidPursuit.Application.Auctions.Querys.GetAllAuctions;
using bidPursuit.Application.Auctions.Querys.GetAuctionById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace bidPursuit.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuctionController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateAuction([FromBody] CreateAuctionCommand createAuctionCommand)
        {
            var result = await mediator.Send(createAuctionCommand);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAuctions([FromQuery] GetAllAuctionsQuery getAllAuctionsQuery)
        {
            var result = await mediator.Send(getAllAuctionsQuery);
            return Ok(result);
        }


        [HttpGet("${AuctionId:guid}")]
        public async Task<IActionResult> GetAuctionById(Guid AuctionId)
        {
            var result = await mediator.Send(new GetAuctionByIdQuery(AuctionId));
            return Ok(result);
        }
    }
}
