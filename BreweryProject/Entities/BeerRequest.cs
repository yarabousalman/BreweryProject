using BreweryProject.Interfaces;

namespace BreweryProject.Entities
{
    public class BeerRequest : IEntity
    {
        public int Id { get; set; }

        public int Amount { get; set; }
    }
}
