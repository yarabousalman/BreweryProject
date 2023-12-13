using BreweryProject.Entities;
using System.ComponentModel.DataAnnotations.Schema;


namespace BreweryProject
{
    public class Beer : IEntity
    {
        public int BeerId {get;set;}
        public string Name { get; set; }
        public int BreweryId { get; set; }
        public decimal AlcoholContent { get;set; }
        public decimal Price { get; set; }
        public int Id { get => BeerId; }
    }
}