namespace NZWalks.Models.DTOs
{
    // user see this formate from region Table 
    public class RegionDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string ImageURL { get; set; } 

    }
}
