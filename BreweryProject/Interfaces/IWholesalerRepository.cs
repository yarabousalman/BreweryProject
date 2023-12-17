using BreweryProject.Data;
using BreweryProject.Entities;

namespace BreweryProject.Interfaces
{
    public interface IWholesalerRepository
    {
        Task<DataResult<QuoteResult>> RequestQuote(QuoteRequest quoteRequest);
    }
}
