using BreweryProject.Interfaces;

namespace BreweryProject.Entities
{
    public class SaleOrder : IEntity
    {
        public int Id { get; set; }

        public int BeerId { get; set; }

        public int WholesalerId { get; set; }

        public int Amount { get; set; }
    }
}
