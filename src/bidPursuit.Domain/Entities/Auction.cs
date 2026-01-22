using bidPursuit.Domain.Enums;

namespace bidPursuit.Domain.Entities;

public class Auction
{
    public Guid Id { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    public AuctionState State { get; set; }
    public string Title { get; set; } = default!;
    public ICollection<AuctionParticipant> Participants { get; set; } = [];
    public ICollection<Vehicle> Vehicles { get; set; } = [];
    public Guid CurrentCarId { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public string Location { get; set; } = default!;
    public string Description { get; set; } = default!;
    public Guid OrganizerId { get; set; }
    public User Organizer { get; set; } = default!;
    public uint Xmin { get; set; }

    public void AddVehicle(Vehicle vehicle, int lotNumber)
    {
        var position = Vehicles.Count + 1;
        vehicle.JoinAuction(this, lotNumber, position);
        Vehicles.Add(vehicle);
    }

    public void StartIfReady(DateTime now)
    {
        if (State != AuctionState.Scheduled)
            return;

        if (StartTime > now)
            return;

        State = AuctionState.InProgress;
        IsActive = true;
        UpdatedAt = now;
    }

    public Vehicle MoveToNextCar()
    {
        if (State != AuctionState.InProgress)
            throw new InvalidOperationException("Auction is not in progress.");

        var orderedVehicles = Vehicles
            .OrderBy(v => v.PositionInAuctionList)
            .ToList();

        if (!orderedVehicles.Any())
            throw new InvalidOperationException("Auction has no vehicles.");

        // First car (auction just started)
        if (CurrentCarId == Guid.Empty)
        {
            var first = orderedVehicles.First();
            CurrentCarId = first.Id;
            UpdatedAt = DateTime.UtcNow;
            return first;
        }

        var currentIndex = orderedVehicles
            .FindIndex(v => v.Id == CurrentCarId);

        if (currentIndex == -1)
            throw new InvalidOperationException("Current car not found in auction.");

        // No more cars → auction finished
        if (currentIndex + 1 >= orderedVehicles.Count)
        {
            EndAuction();
            throw new InvalidOperationException("No more vehicles in auction.");
        }

        var next = orderedVehicles[currentIndex + 1];
        CurrentCarId = next.Id;
        UpdatedAt = DateTime.UtcNow;

        return next;
    }

    private void EndAuction()
    {
        State = AuctionState.Closed;
        IsActive = false;
        UpdatedAt = DateTime.UtcNow;
    }

}
