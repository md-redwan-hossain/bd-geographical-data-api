using Microsoft.AspNetCore.Mvc;

namespace BdGeographicalData.Shared;

public class ApiPagination
{
    [FromQuery(Name = Constants.PageQueryKey)]
    public ushort Page { get; set; }

    [FromQuery(Name = Constants.LimitQueryKey)]
    public ushort Limit { get; set; }
}