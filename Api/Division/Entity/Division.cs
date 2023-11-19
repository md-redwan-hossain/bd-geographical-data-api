using System.ComponentModel.DataAnnotations;

namespace BdRegionalData.Api.Division.Entity;

public class Division 
{
    public int Id { get; set; }
    [MaxLength(50)] public string EnglishName { get; set; }
    [MaxLength(50)] public string BanglaName { get; set; }
    public ICollection<District.Entity.District> Districts { get; set; }
}