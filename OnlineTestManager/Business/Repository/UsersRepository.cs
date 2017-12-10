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
    public class UsersRepository : IUsersRepository
    {
        private readonly DatabaseContext _context;

        public  UsersRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(Guid id)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User> InsertUserAsync(User user)
        {
            _context.Users.Add(user);
            await  _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
           _context.Users.Update(user);
           return await _context.SaveChangesAsync() > 0;
           
        }

        public async Task<bool>  DeleteUserAsync(Guid id)
        {
            _context.Users.Remove(_context.Users.FirstOrDefault(u=>u.Id==id));
            return await _context.SaveChangesAsync()>0;
            
        }

    }
}
