using NZWalks.API.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    public class UpdateRegionRequestDto
    {
        [Required]
        [MinLength(4, ErrorMessage = "code has to be minimum of 4 charaters")]
        [MaxLength(4, ErrorMessage = "code has to be maxium of 4 charaters")]
        public string Code { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "code has to be maxium of 50 charaters")]
        public string name { get; set; }    
        public string? RegionImageUrl { get; set; }
    }
}
