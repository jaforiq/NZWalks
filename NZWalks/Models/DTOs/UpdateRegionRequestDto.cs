using System.ComponentModel.DataAnnotations;

namespace NZWalks.Models.DTOs
{
    // User can update these things
    public class UpdateRegionRequestDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "Code has to be a minimum 3 character.")]
        [MaxLength(3, ErrorMessage = "Code has to be max Length 3 character.")]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
        public string ImageURL { get; set; }
    }
}
