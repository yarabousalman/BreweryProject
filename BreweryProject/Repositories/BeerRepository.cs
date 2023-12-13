using BreweryProject.DataManagers.Data;
using BreweryProject.DataManagers.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BreweryProject.DataManagers.Repositories
{
    public class BeerRepository : GenericRepository<Beer>, IBeerRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public BeerRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Beer> GetBeersByBrewery(int breweryId)
        {
            return _dbContext.Set<Beer>()
                       .AsNoTracking()
                       .Where(e => e.BreweryId == breweryId);
        }
    }
}
