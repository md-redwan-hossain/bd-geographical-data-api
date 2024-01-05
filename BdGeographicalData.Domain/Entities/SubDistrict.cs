namespace BdGeographicalData.Domain.Entities;

public class SubDistrict : IEntity<int>
{
    public int Id { get; set; }
    public int DistrictId { get; set; }
    public int DivisionId { get; set; }
    public string EnglishName { get; set; }
    public string BanglaName { get; set; }
}