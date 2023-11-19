namespace BdRegionalData.Shared;

public static class Helper
{
    public static string? GetDbConnectionString(WebApplicationBuilder x) =>
        x?.Configuration.GetConnectionString("DATABASE_URL");
}