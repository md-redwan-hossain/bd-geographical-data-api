namespace BdGeographicalData.Shared;

public readonly record struct Constants
{
    public const string AppOptions = "AppOptions";
    public const string SecretsJsonFileName = "secrets.json";
    public const string PageQueryKey = "page";
    public const string LimitQueryKey = "limit";
    public const string SortingQueryKey = "sort_order";

    public const string SerilogFilePath = "Logs/web-log.log";

    public const string SerilogConsoleOutputTemplate =
        "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} <s:{SourceContext}>{NewLine}{Exception}";
}