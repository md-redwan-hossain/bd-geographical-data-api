using Microsoft.AspNetCore.Mvc;

namespace BdGeographicalData.Shared;

public class ApiPagination
{
    [FromQuery(Name = Constant.PageQueryKey)]
    public ushort Page { get; set; }

    [FromQuery(Name = Constant.LimitQueryKey)]
    public ushort Limit { get; set; }
}