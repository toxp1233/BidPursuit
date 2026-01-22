namespace bidPursuit.Application.PublicDtos;

public class VehicleDto
{
    public Guid Id { get; set; }
    public string Brand { get; set; } = default!;
    public string Model { get; set; } = default!;
    public int Year { get; set; }

    public decimal StartingPrice { get; set; }
    public decimal BuyNowPrice { get; set; }

    public int LotNumber { get; set; }
    public int PositionInAuctionList { get; set; }

    public bool IsSold { get; set; }
    public string CurrentLocation { get; set; } = default!;
    public List<BidDto>? Bids { get; set; }

    public UserDto Publisher { get; set; } = default!;
}
