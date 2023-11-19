using System.ComponentModel.DataAnnotations;

namespace BdRegionalData.Api.District.Entity;

public class District 
{
    public int Id { get; set; }
    public int DivisionId { get; set; }
    [MaxLength(50)] public string EnglishName { get; set; }
    [MaxLength(50)] public string BanglaName { get; set; }
    public Division.Entity.Division Division { get; set; }
    public ICollection<SubDistrict.Entity.SubDistrict> SubDistricts { get; set; }
} 