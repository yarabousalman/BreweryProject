namespace BreweryProject.DataManagers.Interfaces
{
    public interface IBeerRepository : IGenericRepository<Beer>
    {
        IQueryable<Beer> GetBeersByBrewery(int breweryId);
    }
}
