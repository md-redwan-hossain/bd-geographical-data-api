﻿namespace BdGeographicalData.Application;

public class DistrictResponseDto
{
  public int Id { get; set; }
  public int DivisionId { get; set; }
  public string EnglishName { get; set; }
  public string BanglaName { get; set; }
  public List<SubDistrictResponseDto> SubDistricts { get; set; }

}