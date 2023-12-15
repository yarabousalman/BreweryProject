namespace BreweryProject.Entities
{
    public class Wholesaler : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<SaleOrder> SaleOrders { get; set; }
        public List<Stock> Stocks { get; set; }

    }
}
