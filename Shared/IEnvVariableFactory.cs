namespace BdGeographicalData.Shared;

public interface IEnvVariableFactory
{
    public EnvVariable CreateOrGet();
}