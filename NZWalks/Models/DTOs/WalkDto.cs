namespace NZWalks.Models.DTOs;

public class WalkDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double LengthInKM { get; set; }
    public string? WalkImageURL { get; set; }

    public Guid DifficultyId { get; set; }
    public Guid RegionId { get; set; }
}

