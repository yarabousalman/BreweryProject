using BreweryProject.Entities;
using BreweryProject.Interfaces;
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

    }
}