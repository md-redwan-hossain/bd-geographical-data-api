using Microsoft.AspNetCore.Mvc;

namespace BdGeographicalData.Shared;

public class ApiPagination
{
    [FromQuery(Name = "page")] public ushort Page { get; set; }
    [FromQuery(Name = "limit")] public ushort Limit { get; set; }
}
