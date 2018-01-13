using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Core.Domain;

namespace Data.Core.Interfaces
{
    public interface IGenericRepository<T> where T : IBaseEntity
    {
        Task<bool> DeleteAsync(Guid id);
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(Guid id);
        Task<T> InsertAsync(T testInstance);
        Task<bool> UpdateAsync(T entity);
    }
}