using BreweryProject.Data;
using BreweryProject.Entities;

namespace BreweryProject.DataManagers.Interfaces
{
    public interface IBeerRepository : IGenericRepository<Beer>
    {
        Task<DataResult<IQueryable<Beer>>> GetBeersByBrewery(int breweryId);

        Task<DataResult<Beer>> CreateBeer(Beer beerToAdd);
    }
}
