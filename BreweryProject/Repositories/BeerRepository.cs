﻿using BreweryProject.Data;
using BreweryProject.DataManagers.Data;
using BreweryProject.DataManagers.Interfaces;
using BreweryProject.Entities;
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

        public async Task<DataResult<Beer>> CreateBeer(Beer beerToAdd)
        {
            var dataResult = new DataResult<Beer>();
            try
            {
                var breweryRepo = new GenericRepository<Brewery>(_dbContext);
                var brewery = breweryRepo.GetById(beerToAdd.BreweryId);
                if (brewery.Result.Data != null)
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

        public DataResult<IQueryable<Beer>> GetBeersByBrewery(int breweryId)
        {
            var dataResult = new DataResult<IQueryable<Beer>>();
            try
            {
                dataResult.Data = _dbContext.Set<Beer>()
                      .AsNoTracking()
                      .Where(e => e.BreweryId == breweryId);

            }
            catch(Exception ex)
            {
                dataResult.ErrorMessage = JsonConvert.SerializeObject(ex);
            }
            return dataResult;
        }
    }
}