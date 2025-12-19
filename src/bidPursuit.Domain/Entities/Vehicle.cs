namespace bidPursuit.Domain.Entities;

public class Vehicle
{
    public Guid Id { get; set; }
    public string Brand { get; set; } = default!;
    public string Model { get; set; } = default!;
    public int Year { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public decimal StartingPrice { get; set; } 
    public decimal BuyNowPrice { get; set; }
    public int LotNumber { get; set; }
    public int PositionInAuctionList { get; set; }
    public Guid AuctionId { get; set; }
    public Auction Auction { get; set; } = default!;
    public string CurrentLocation { get; set; } = default!;
    public bool IsSold { get; set; }
    public Guid UserId { get; set; }
    public User Publisher { get; set; } = default!;
    public ICollection<Bid> Bids { get; set; } = [];
}
