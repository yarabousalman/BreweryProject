using BreweryProject.Data;
using BreweryProject.DataManagers.Data;
using BreweryProject.DataManagers.Interfaces;
using BreweryProject.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace BreweryProject.DataManagers.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, IEntity
    {
        private readonly ApplicationDbContext _dbContext;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public DataResult<IQueryable<T>> GetAll()
        {
            var dataResult = new DataResult<IQueryable<T>>();
            try
            {
                dataResult.Data = _dbContext.Set<T>().AsNoTracking();
            }
            catch(Exception ex)
            {
                dataResult.ErrorMessage = JsonConvert.SerializeObject(ex);
            }
            return dataResult;
        }

        public async Task<DataResult<T>> GetById(int id)
        {
            var dataResult = new DataResult<T>();
            try
            {
                dataResult.Data = await _dbContext.Set<T>()
                        .AsNoTracking()
                        .FirstOrDefaultAsync(e => e.Id == id);
            }
            catch (Exception ex)
            {
                dataResult.ErrorMessage = JsonConvert.SerializeObject(ex);
            }
            return dataResult;
        }

        public async Task<DataResult<T>> Create(T entity)
        {
            var dataResult = new DataResult<T>();
            try
            {
                await _dbContext.Set<T>().AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                dataResult.Data = entity;
            }
            catch (Exception ex)
            {
                dataResult.ErrorMessage = JsonConvert.SerializeObject(ex);
            }
            return dataResult;
        }

        public async Task<DataResult<T>> Update(T entity)
        {
            var dataResult = new DataResult<T>();
            try
            {
                _dbContext.Set<T>().Update(entity);
                await _dbContext.SaveChangesAsync();
                dataResult.Data = entity;
            }
            catch (Exception ex)
            {
                dataResult.ErrorMessage = JsonConvert.SerializeObject(ex);
            }
            return dataResult;
        }

        public async Task<DataResult<string>> Delete(int id)
        {
            var dataResult = new DataResult<string>();
            try
            {
                var entity = await GetById(id);
                if(entity.Data!=null)
                {
                    _dbContext.Set<T>().Remove(entity.Data);
                    await _dbContext.SaveChangesAsync();
                    dataResult.Data = "Record deleted successfully.";
                }
                else
                {
                    dataResult.ErrorMessage = "Record does not exist.";
                }
               
            }
            catch (Exception ex)
            {
                dataResult.ErrorMessage = JsonConvert.SerializeObject(ex);
            }
            return dataResult;
        }
    }
}
