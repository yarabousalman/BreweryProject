using BreweryProject.Data;
using BreweryProject.Entities;

namespace BreweryProject.DataManagers.Interfaces
{
    public interface IGenericRepository<T> where T : class, IEntity
    {
        DataResult<IQueryable<T>> GetAll();

        Task<DataResult<T>> GetById(int id);

        Task<DataResult<T>> Create(T entity);

        Task<DataResult<T>> Update(T entity);

        Task<DataResult<string>> Delete(int id);
    }
}
