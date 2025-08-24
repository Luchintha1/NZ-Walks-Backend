using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    public class UpdateRegionDTO
    {
        [Required]
        [MinLength(2, ErrorMessage = "The code can have minimum of 2 characters")]
        [MaxLength(3, ErrorMessage = "The code can have maximum of 3 characters")]
        public string code { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "The code can have maximum of 100 characters")]
        public string Name { get; set; }

        public string? RegionImageUrl { get; set; }
    }
}
