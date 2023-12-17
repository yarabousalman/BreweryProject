using BreweryProject.Interfaces;

namespace BreweryProject.Entities
{
    public class QuoteResult : IEntity
    {
        public int Id { get; set; }

        public decimal Price { get; set; }

        public string Summary { get; set; }
    }
}
