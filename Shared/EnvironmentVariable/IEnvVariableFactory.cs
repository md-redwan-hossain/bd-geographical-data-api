namespace BdGeographicalData.Shared.EnvironmentVariable;

public interface IEnvVariableFactory
{
    public IEnvVariable CreateOrGet();
}