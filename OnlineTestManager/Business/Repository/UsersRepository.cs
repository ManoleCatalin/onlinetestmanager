using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Repository.Base;
using Data.Core.Domain;
using Data.Core.Interfaces;
using Data.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Business.Repository
{
    public class UsersRepository : GenericRepository<User>, IUsersRepository
    {
        public UsersRepository(DatabaseContext context) : base(context)
        {
        }

        public override async Task<List<User>> GetAllAsync()
        {
            return await _entities.Include(user => user.UserType).ToListAsync();
        }

        public override async Task<User> GetByIdAsync(Guid id)
        {
            return await _entities.Include(user => user.UserType).FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
