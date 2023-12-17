using BreweryProject.Data;
using BreweryProject.DataManagers.Data;
using BreweryProject.DataManagers.Repositories;
using BreweryProject.Entities;
using BreweryProject.Interfaces;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace BreweryProject.Repositories
{
    public class StockRepository : GenericRepository<Stock>, IStockRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public StockRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<DataResult<Stock>> UpdateStock(Stock stockToUpdate)
        {
            var dataResult = new DataResult<Stock>();
            try
            {
                var beerRepo = new BeerRepository(_dbContext);
                var beer = await beerRepo.GetById(stockToUpdate.BeerId);
                var wholesalerRepo = new GenericRepository<Wholesaler>(_dbContext);
                var wholesaler = await wholesalerRepo.GetById(stockToUpdate.WholesalerId);

                if (beer.Data == null)
                {
                    dataResult.ErrorMessage = "Beer does not exist.";
                }
                else if (wholesaler.Data == null)
                {
                    dataResult.ErrorMessage = "Wholesaler does not exist.";
                }
                else
                {
                    var stock = await GetStockByBeerId(stockToUpdate.BeerId);
                    if (stock.Data != null)
                    {
                        stockToUpdate.Id = stock.Data.Id;
                        dataResult = await Update(stockToUpdate);
                    }
                    else
                    {
                        dataResult.ErrorMessage = "Stock for this beer does not exist.";
                    }
                }
            }
            catch (Exception ex)
            {
                dataResult.ErrorMessage = JsonConvert.SerializeObject(ex);
            }
            return dataResult;
        }

        public async Task<DataResult<Stock>> GetStockByBeerId(int beerId)
        {
            var dataResult = new DataResult<Stock>();
            try
            {
                dataResult.Data = await _dbContext.Set<Stock>()
                        .AsNoTracking()
                        .FirstOrDefaultAsync(e => e.BeerId == beerId);
            }
            catch (Exception ex)
            {
                dataResult.ErrorMessage = JsonConvert.SerializeObject(ex);
            }
            return dataResult;

        }

        public async Task<DataResult<Stock>> AddToStock(Stock stockToUpdate)
        {
            var dataResult = new DataResult<Stock>();
            try
            {
                var stock = await GetStockByBeerId(stockToUpdate.BeerId);
                if (stock.Data != null)
                {
                    stockToUpdate.Id = stock.Data.Id;
                    stockToUpdate.Amount += stockToUpdate.Amount;
                    dataResult = await Update(stockToUpdate);
                }
                else
                {
                    dataResult = await Create(stockToUpdate);
                }
            }
            catch (Exception ex)
            {
                dataResult.ErrorMessage = JsonConvert.SerializeObject(ex);
            }
            return dataResult;
        }

        public async Task<DataResult<Stock>> RemoveFromStock(Stock stockToUpdate)
        {
            var dataResult = new DataResult<Stock>();
            try
            {
                var stock = await GetStockByBeerId(stockToUpdate.BeerId);
                stockToUpdate.Id = stock.Data.Id;
                stockToUpdate.Amount -= stockToUpdate.Amount;
                dataResult = await Update(stockToUpdate);
            }
            catch (Exception ex)
            {
                dataResult.ErrorMessage = JsonConvert.SerializeObject(ex);
            }
            return dataResult;
        }
    }
}
