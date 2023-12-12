using BreweryProject.DataManagers.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BreweryProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BeerController : ControllerBase
    {
        private IGenericRepository<Beer> _beerRepository;
        public BeerController(IGenericRepository<Beer> beerRepository)
        {
            _beerRepository = beerRepository;
        }

        [HttpGet(Name = "GetAllBeers")]
        public IEnumerable<Beer> GetAllBeers()
        {
            return _beerRepository.GetAll();
        }
    }
}