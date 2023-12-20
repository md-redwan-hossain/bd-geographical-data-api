using System.ComponentModel.DataAnnotations;

namespace BdGeographicalData.Shared;

public class AppOptions
{
    [Required] public required string? DatabaseUrl { get; init; }
    [Required] [Range(1, int.MaxValue)] public required int ResponseCacheDurationInSecond { get; init; }
}