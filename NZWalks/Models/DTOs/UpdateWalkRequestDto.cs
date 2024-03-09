using System.ComponentModel.DataAnnotations;

namespace NZWalks.Models.DTOs;

public class UpdateWalkRequestDto
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public double LengthInKM { get; set; }
    public string? WalkImageURL { get; set; }

    public Guid DifficultyId { get; set; }
    public Guid RegionId { get; set; }
}

