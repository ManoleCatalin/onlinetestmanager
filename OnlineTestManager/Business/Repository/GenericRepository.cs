using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Core.Domain;
using Data.Core.Interfaces;
using Data.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Business.Repository
{
    public  class GenericRepository<T> : IGenericRepository<T> where T :BaseEntity
    {
        private readonly DatabaseContext _context;
        private readonly DbSet<T> _entities;

        public GenericRepository(DatabaseContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }


        public async Task<List<T>> GetAllAsync() => await _entities.ToListAsync();

        public async Task<T> GetByIdAsync(Guid id) => await _entities.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<T> InsertAsync(T entity)
        {
            _entities.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            _entities.Update(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            _entities.Remove(_entities.FirstOrDefault(u => u.Id == id));
            return await _context.SaveChangesAsync() > 0;
        }

    }
}
