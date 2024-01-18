namespace NZWalks.Models.DTOs
{
    // User can update these things
    public class UpdateRegionRequestDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string ImageURL { get; set; }
    }
}
