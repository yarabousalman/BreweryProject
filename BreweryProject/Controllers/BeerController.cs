using BreweryProject.DataManagers.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BreweryProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BeerController : ControllerBase
    {
        private IGenericRepository<Beer> _beerRepository;
        private IBeerRepository _iBeerRepository;
        public BeerController(IGenericRepository<Beer> beerRepository, IBeerRepository iBeerRepository)
        {
            _beerRepository = beerRepository;
            _iBeerRepository = iBeerRepository;
        }

        [HttpGet(Name = "GetAll")]
        public IEnumerable<Beer> GetAll()
        {
            return _beerRepository.GetAll();
        }

        //[HttpGet(Name = "GetBeersByBrewery")]
        //public IEnumerable<Beer> GetBeersByBrewery(int breweryId)
        //{
        //    return _iBeerRepository.GetBeersByBrewery(breweryId);
        //}
    }
}