namespace BreweryProject.Entities
{
    public class Brewery : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Beer> Beers { get; set; }
    }
}
