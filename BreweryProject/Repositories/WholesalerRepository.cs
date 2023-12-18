using BreweryProject.Data;
using BreweryProject.DataManagers.Data;
using BreweryProject.DataManagers.Repositories;
using BreweryProject.Entities;
using BreweryProject.Interfaces;
using System.Text;

namespace BreweryProject.Repositories
{
    public class WholesalerRepository : GenericRepository<Wholesaler>, IWholesalerRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public WholesalerRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        //When a quote request is successful, the stocks relating to this requested are decreased
        //to reflect the amount that was sold
        public async Task<DataResult<QuoteResult>> RequestQuote(QuoteRequest quoteRequest)
        {
            var dataResult = new DataResult<QuoteResult>();
            var wholesalerRepo = new WholesalerRepository(_dbContext);
            var wholesaler = await wholesalerRepo.GetById(quoteRequest.WholesalerId);
            decimal total = 0;
            int drinkCount = 0;
            var beersSold = new Dictionary<int, int>();
            if (quoteRequest.BeerRequests == null || !quoteRequest.BeerRequests.Any())
            {
                dataResult.ErrorMessage = "The order cannot be empty.";
            }
            else if (wholesaler.Data == null)
            {
                dataResult.ErrorMessage = "The wholesaler does not exist.";
            }
            else if (quoteRequest.BeerRequests.GroupBy(_ => _.Id).ToList().Count != quoteRequest.BeerRequests.Count)
            {
                dataResult.ErrorMessage = "There can't be any duplicate in the order.";
            }
            else
            {
                var stockRepo = new StockRepository(_dbContext);
                var beerRepo = new BeerRepository(_dbContext);
                foreach (var requestedBeer in quoteRequest.BeerRequests)
                {
                    var stock = await stockRepo.GetStockByBeerId(requestedBeer.Id);
                    if (stock.Data == null)
                    {
                        dataResult.ErrorMessage = $"Beer having Id {requestedBeer.Id} does not exist in the stock.";
                        break;
                    }
                    else if (stock.Data.Amount < requestedBeer.Amount)
                    {
                        dataResult.ErrorMessage = $"There is not enough stock for beer having Id {requestedBeer.Id}.";
                        break;
                    }
                    else
                    {
                        var beer = await beerRepo.GetById(requestedBeer.Id);
                        if (beer.Data != null)
                        {
                            total += beer.Data.Price * requestedBeer.Amount;
                            drinkCount += requestedBeer.Amount;
                            beersSold.Add(beer.Data.Id, requestedBeer.Amount);
                        }
                        else
                        {
                            dataResult.ErrorMessage = $"Beer having Id {requestedBeer.Id} does not exist.";
                            break;
                        }
                    }
                }
                if (dataResult.ErrorMessage == null)
                {
                    if (drinkCount > 10 && drinkCount <= 20)
                    {
                        total = total * (decimal)0.9;
                    }
                    else if (drinkCount > 20)
                    {
                        total = total * (decimal)0.8;
                    }
                    dataResult.Data = new QuoteResult
                    {
                        Price = total
                    };
                    var quoteSummary = new StringBuilder();
                    foreach (var soldBeer in beersSold)
                    {
                        quoteSummary.Append($"{soldBeer.Value} beers having Id {soldBeer.Key}. ");

                        await stockRepo.RemoveFromStock(new Stock
                        {
                            WholesalerId = quoteRequest.WholesalerId,
                            BeerId = soldBeer.Key,
                            Amount = soldBeer.Value
                        });
                    }
                    quoteSummary.Append($"For a total of {total}");
                    dataResult.Data.Summary = quoteSummary.ToString();
                }
            }
            return dataResult;
        }
    }
}
