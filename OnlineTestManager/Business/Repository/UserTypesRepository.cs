using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Core.Domain;
using Data.Core.Interfaces;
using Data.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Business.Repository
{
    public class UserTypesRepository : IUserTypesRepository
    {
        private readonly DatabaseContext _context;

        public UserTypesRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<UserType>> GetUserTypesAsync()
        {
            return await _context.UserTypes.ToListAsync();
        }

        public async Task<UserType> GetUserTypeByIdAsync(Guid id)
        {
            return await _context.UserTypes.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<UserType> InsertUserTypeAsync(UserType userType)
        {
            _context.UserTypes.Add(userType);
            await _context.SaveChangesAsync();
            return userType;
        }

        public async Task<bool> UpdateUserTypeAsync(UserType userType)
        {
            _context.UserTypes.Update(userType);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteUserTypeAsync(Guid id)
        {
            _context.UserTypes.Remove(_context.UserTypes.FirstOrDefault(t => t.Id == id));
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
