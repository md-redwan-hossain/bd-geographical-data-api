using Serilog.Sinks.SystemConsole.Themes;

namespace BdGeographicalData.Shared;

public static class SerilogConfig
{
    public const string OutputTemplate =
        "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} <s:{SourceContext}>{NewLine}{Exception}";
}