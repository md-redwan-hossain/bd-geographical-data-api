namespace BdGeographicalData.Domain.Entities
{
    public interface IEntity<T>
        where T : notnull
    {
        T Id { get; set; }
    }
}