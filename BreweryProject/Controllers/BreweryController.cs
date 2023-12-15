using BreweryProject.Data;
using BreweryProject.DataManagers.Interfaces;
using BreweryProject.Entities;
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

        public BreweryController(IGenericRepository<Beer> beerRepository, IBeerRepository iBeerRepository, IGenericRepository<Brewery> breweryRepository)
        {
            _beerRepository = beerRepository;
            _iBeerRepository = iBeerRepository;
            _breweryRepository = breweryRepository;
        }

        [HttpGet("GetAllBeers")]
        public DataResult<IQueryable<Beer>> GetAllBeers()
        {
            return _beerRepository.GetAll();
        }

        [HttpPost("CreateBeer")]
        public Task<DataResult<Beer>> CreateBeer(Beer beerToAdd)
        {
            return _iBeerRepository.CreateBeer(beerToAdd);
        }

        [HttpGet("DeleteBeer")]
        public Task<DataResult<string>> DeleteBeer(int beerId)
        {
            return _beerRepository.Delete(beerId);
        }

        [HttpGet("GetBeersByBrewery")]
        public DataResult<IQueryable<Beer>> GetBeersByBrewery(int breweryId)
        {
            return _iBeerRepository.GetBeersByBrewery(breweryId);
        }
    }
}