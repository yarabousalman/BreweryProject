using BreweryProject.Data;

namespace BreweryProject.DataManagers.Interfaces
{
    public interface IBeerRepository : IGenericRepository<Beer>
    {
        DataResult<IQueryable<Beer>> GetBeersByBrewery(int breweryId);

        Task<DataResult<Beer>> CreateBeer(Beer beerToAdd);
    }
}
