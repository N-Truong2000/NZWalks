using System.Text.Json.Serialization;

namespace NZWalks.API.Models.DTO
{
    [Serializable]
    public class RegionDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public string? RegionImageUrl { get; set; }
    }
}
