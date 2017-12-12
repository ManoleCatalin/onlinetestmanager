using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Core.Domain;

namespace Data.Core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<bool> DeleteAsync(Guid id);
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(Guid id);
        Task<T> InsertAsync(T entity);
        Task<bool> UpdateAsync(T entity);
    }
}