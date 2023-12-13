﻿using BreweryProject.Entities;

namespace BreweryProject.DataManagers.Interfaces
{
    public interface IGenericRepository<T> where T : class, IEntity
    {
        IQueryable<T> GetAll();

        Task<T> GetById(int id);

        Task Create(T entity);

        Task Update(int id, T entity);

        Task Delete(int id);
    }
}
