using BreweryProject.Data;
using BreweryProject.Entities;

namespace BreweryProject.Interfaces
{
    public interface ISaleOrderRepository
    {
        Task<DataResult<SaleOrder>> CreateSaleOrder(SaleOrder saleOrderToAdd);

    }
}
