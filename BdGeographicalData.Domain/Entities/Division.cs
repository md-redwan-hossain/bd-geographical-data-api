namespace BdGeographicalData.Domain.Entities
{
    public class Division : IEntity<int>
    {
        public int Id { get; set; }
        public string EnglishName { get; set; }
        public string BanglaName { get; set; }
    }
}