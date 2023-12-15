using BreweryProject.Entities;
using System.ComponentModel.DataAnnotations.Schema;


namespace BreweryProject
{
    public class Beer : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BreweryId { get; set; }
        public decimal AlcoholContent { get; set; }
        public decimal Price { get; set; }
        public List<SaleOrder> SaleOrders { get; set; }
        public List<Stock> Stocks { get; set; }
    }
}