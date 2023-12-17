using BreweryProject.Interfaces;

namespace BreweryProject.Entities
{
    public class QuoteRequest : IEntity
    {
        public int Id { get; set; }

        public int WholesalerId { get; set; }

        public List<BeerRequest> BeerRequests { get; set; }

    }
}
