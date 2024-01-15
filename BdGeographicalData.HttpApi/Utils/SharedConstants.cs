namespace BdGeographicalData.HttpApi.Utils
{
    public readonly record struct SharedConstants
    {
        public const string AppOptions = "AppOptions";
        public const string SecretsJsonFileName = "secrets.json";
        public const string SerilogFilePath = "Logs/web-log.log";
        public const string SerilogConsoleOutputTemplate =
            "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} <s:{SourceContext}>{NewLine}{Exception}";
    }
}