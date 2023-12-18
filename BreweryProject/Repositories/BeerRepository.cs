using BreweryProject.Data;
using BreweryProject.DataManagers.Data;
using BreweryProject.DataManagers.Interfaces;
using BreweryProject.Entities;
using BreweryProject.Repositories;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace BreweryProject.DataManagers.Repositories
{
    public class BeerRepository : GenericRepository<Beer>, IBeerRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public BeerRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        //This method checks if beer is linked to a brewery
        //If not, a default brewery is created
        public async Task<DataResult<Beer>> CreateBeer(Beer beerToAdd)
        {
            var dataResult = new DataResult<Beer>();
            try
            {
                var breweryRepo = new GenericRepository<Brewery>(_dbContext);
                var brewery = await breweryRepo.GetById(beerToAdd.BreweryId);
                if (brewery.Data != null)
                {
                    dataResult = await Create(beerToAdd);
                }
                else
                {
                    var addedBrewery = await breweryRepo.Create(new Brewery
                    {
                        Name = $"{beerToAdd.Name} Brewery",
                    });
                    if (addedBrewery.Data != null)
                    {
                        beerToAdd.BreweryId = addedBrewery.Data.Id;
                        dataResult = await Create(beerToAdd);
                    }
                    else
                    {
                        dataResult.ErrorMessage = addedBrewery.ErrorMessage;
                    }
                }

            }
            catch (Exception ex)
            {
                dataResult.ErrorMessage = JsonConvert.SerializeObject(ex);
            }
            return dataResult;
        }

        //For simplicity, it is assumed that the brewery Id is known
        public async Task<DataResult<IQueryable<Beer>>> GetBeersByBrewery(int breweryId)
        {
            var dataResult = new DataResult<IQueryable<Beer>>();
            try
            {
                var breweryRepo = new GenericRepository<Brewery>(_dbContext);
                var brewery = await breweryRepo.GetById(breweryId);
                if(brewery.Data == null)
                {
                    dataResult.ErrorMessage = "Brewery does not exist.";
                }
                else
                {
                    dataResult.Data = _dbContext.Set<Beer>()
                     .AsNoTracking()
                     .Where(e => e.BreweryId == breweryId);
                }
            }
            catch(Exception ex)
            {
                dataResult.ErrorMessage = JsonConvert.SerializeObject(ex);
            }
            return dataResult;
        }
    }
}
