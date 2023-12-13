namespace BreweryProject.Entities
{
    public class Wholesaler : IEntity
    {
        public int WholesalerId { get; set; }
        public int Id => WholesalerId;
    }
}
