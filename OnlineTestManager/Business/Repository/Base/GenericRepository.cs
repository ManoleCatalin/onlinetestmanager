using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Core.Domain;
using Data.Core.Interfaces;
using Data.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Business.Repository.Base
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, IBaseEntity
    {
        protected readonly DatabaseContext _context;
        protected readonly DbSet<T> _entities;

        public GenericRepository(DatabaseContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        public virtual async Task<List<T>> GetAllAsync() => await _entities.Where(x => !x.IsDeleted).ToListAsync();

        public virtual async Task<T> GetByIdAsync(Guid id) => await _entities.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

        public virtual async Task<T> InsertAsync(T entity)
        {
            _entities.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<bool> UpdateAsync(T entity)
        {
            _entities.Update(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public virtual async Task<bool> DeleteAsync(Guid id)
        {
           var entity=_entities.FirstOrDefault(u => u.Id == id);
            if (entity == null) return await _context.SaveChangesAsync() > 0;
            entity.IsDeleted = true;
            _entities.Update(entity);
            return await _context.SaveChangesAsync() > 0;
        }

    }
}
