using BreweryProject.Data;
using BreweryProject.Entities;

namespace BreweryProject.Interfaces
{
    public interface IStockRepository
    {
        Task<DataResult<Stock>> UpdateStock(Stock stockToUpdate, bool isSaleOrder = false);
    }
}
