using BreweryProject.Data;
using BreweryProject.DataManagers.Data;
using BreweryProject.DataManagers.Repositories;
using BreweryProject.Entities;
using BreweryProject.Interfaces;
using Newtonsoft.Json;

namespace BreweryProject.Repositories
{
    public class SaleOrderRepository : GenericRepository<SaleOrder>, ISaleOrderRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public SaleOrderRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<DataResult<SaleOrder>> CreateSaleOrder(SaleOrder saleOrderToAdd)
        {
            var dataResult = new DataResult<SaleOrder>();
            try
            {
                var beerRepo = new BeerRepository(_dbContext);
                var beer = await beerRepo.GetById(saleOrderToAdd.BeerId);
                var wholesalerRepo = new GenericRepository<Wholesaler>(_dbContext);
                var wholesaler = await wholesalerRepo.GetById(saleOrderToAdd.WholesalerId);
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
                    dataResult = await Create(saleOrderToAdd);
                    if (dataResult.Data != null)
                    {
                        var stockRepo = new StockRepository(_dbContext);
                        var stockDataResult = await stockRepo.AddToStock(new Stock
                        {
                            BeerId = saleOrderToAdd.BeerId,
                            Amount = saleOrderToAdd.Amount,
                            WholesalerId = saleOrderToAdd.WholesalerId
                        });
                        if (stockDataResult.ErrorMessage != null)
                        {
                            dataResult.ErrorMessage = $"Sale Order was created but the following error occured when updating the stock: {stockDataResult.ErrorMessage}";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dataResult.ErrorMessage = JsonConvert.SerializeObject(ex);
            }
            return dataResult;
        }
    }
}
