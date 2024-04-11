using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    public class UpdateWalkRequestDto
    {
        [Required]
        [MaxLength(50, ErrorMessage = "code has to be maxium of 50 charaters")]
        public string Name { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "code has to be maxium of 50 charaters")]
        public string Description { get; set; }

        [Required]
        [Range(0, 100)]
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        [Required]
        public Guid DifficultyId { get; set; }
        [Required]
        public Guid RegionId { get; set; }
    }
}
