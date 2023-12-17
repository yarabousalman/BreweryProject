using BreweryProject.Data;
using BreweryProject.Entities;

namespace BreweryProject.Interfaces
{
    public interface IStockRepository
    {
        Task<DataResult<Stock>> UpdateStock(Stock stockToUpdate);

        Task<DataResult<Stock>> AddToStock(Stock stockToUpdate);

        Task<DataResult<Stock>> RemoveFromStock(Stock stockToUpdate);

    }
}
