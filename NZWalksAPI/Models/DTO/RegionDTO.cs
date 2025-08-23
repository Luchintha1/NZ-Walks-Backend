namespace NZWalks.API.Models.DTO
{
    public class RegionDTO
    {
        public Guid id { get; set; }

        public string code { get; set; }

        public string Name { get; set; }

        public string? RegionImageUrl { get; set; }
    }
}
