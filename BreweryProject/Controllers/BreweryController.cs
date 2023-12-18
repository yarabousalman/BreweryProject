using BreweryProject.Data;
using BreweryProject.DataManagers.Interfaces;
using BreweryProject.Entities;
using BreweryProject.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BreweryProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BreweryController : ControllerBase
    {
        private IGenericRepository<Beer> _beerRepository;
        private IBeerRepository _iBeerRepository;
        private IGenericRepository<Brewery> _breweryRepository;
        private ISaleOrderRepository _iSaleOrderRepository;
        private IStockRepository _iStockRepository;
        private IWholesalerRepository _iWholesalerRepository;

        public BreweryController(IGenericRepository<Beer> beerRepository, IBeerRepository iBeerRepository, IGenericRepository<Brewery> breweryRepository,
            ISaleOrderRepository iSaleOrderRepository, IStockRepository iStockRepository, IWholesalerRepository iWholesalerRepository)
        {
            _beerRepository = beerRepository;
            _iBeerRepository = iBeerRepository;
            _breweryRepository = breweryRepository;
            _iSaleOrderRepository = iSaleOrderRepository;
            _iStockRepository = iStockRepository;
            _iWholesalerRepository = iWholesalerRepository;
        }

        [HttpGet("GetBeersByBrewery - FR1")]
        public Task<DataResult<IQueryable<Beer>>> GetBeersByBrewery(int breweryId)
        {
            return _iBeerRepository.GetBeersByBrewery(breweryId);
        }

        [HttpPost("CreateBeer - FR2")]
        public Task<DataResult<Beer>> CreateBeer(Beer beerToAdd)
        {
            return _iBeerRepository.CreateBeer(beerToAdd);
        }

        [HttpGet("DeleteBeer - FR3")]
        public Task<DataResult<string>> DeleteBeer(int beerId)
        {
            return _beerRepository.Delete(beerId);
        }

        [HttpPost("CreateSaleOrder - FR4")]
        public Task<DataResult<SaleOrder>> CreateSaleOrder(SaleOrder saleOrderToAdd)
        {
            return _iSaleOrderRepository.CreateSaleOrder(saleOrderToAdd);
        }

        [HttpPost("UpdateStock - FR5")]
        public Task<DataResult<Stock>> UpdateStock(Stock stockToUpdate)
        {
            return _iStockRepository.UpdateStock(stockToUpdate);
        }

        [HttpPost("RequestQuote - FR6")]
        public Task<DataResult<QuoteResult>> RequestQuote(QuoteRequest quoteRequest)
        {
            return _iWholesalerRepository.RequestQuote(quoteRequest);
        }
    }
}