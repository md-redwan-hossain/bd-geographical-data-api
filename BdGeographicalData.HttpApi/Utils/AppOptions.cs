using System.ComponentModel.DataAnnotations;

namespace BdGeographicalData.HttpApi.Utils
{
    public class AppOptions
    {
        [Required]public required string? DatabaseUrl { get; init; }
        [Required] [Range(1, int.MaxValue)] public required int ResponseCacheDurationInSecond { get; init; }
    }
}