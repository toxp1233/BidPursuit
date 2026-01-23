using bidPursuit.Domain.Enums;

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
    public CarState State { get; set; } = CarState.draftable;
    public bool EarlyBiddingEnabled { get; set; } = true;
    public int LotNumber { get; set; }
    public int PositionInAuctionList { get; set; }
    public Guid? AuctionId { get; set; }
    public Auction? Auction { get; set; }
    public string CurrentLocation { get; set; } = default!;
    public bool IsSold { get; set; } = false;
    public Guid UserId { get; set; }
    public User Publisher { get; set; } = default!;
    public ICollection<Bid> Bids { get; set; } = [];
    public List<string> Images { get; set; } = [];
    public void JoinAuction(Auction auction, int lotNumber, int position)
    {
        Auction = auction;
        AuctionId = auction.Id;
        State = CarState.inAuction;
        LotNumber = lotNumber;
        PositionInAuctionList = position;
    }
}

