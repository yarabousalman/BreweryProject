namespace BreweryProject.Entities
{
    public class Brewery : IEntity
    {
        public int BreweryId { get; set; }
        public string Name { get; set; }
        public List<Beer> Beers { get; set; }
        public int Id { get => BreweryId; }
    }
}
