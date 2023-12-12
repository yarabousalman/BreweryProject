using BreweryProject.DataManagers.Data;
using BreweryProject.DataManagers.Interfaces;

namespace BreweryProject.DataManagers.Repositories
{
    public class BeerRepository : GenericRepository<Beer>, IBeerRepository
    {
        public BeerRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
