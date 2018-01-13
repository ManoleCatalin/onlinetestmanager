using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Repository.Base;
using Constants;
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
            return await _entities.Include(user => user.UserRole).ToListAsync();
        }

        public override async Task<User> GetByIdAsync(Guid id)
        {
            return await _entities
                .Include(user => user.UserRole)
                    .ThenInclude(userRole => userRole.Role)
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        }

        public async Task<User> GetByUserNameAsync(string userName)
        {
            return await _entities
                .Include(user => user.UserRole)
                    .ThenInclude(userRole => userRole.Role)
                .FirstOrDefaultAsync(x => x.UserName == userName && !x.IsDeleted);
        }

        public async Task<List<User>> GetStudentsByNamePrefixAsync(string userNamePrefix)
        {
            var studentRoleId = _context.Roles.FirstOrDefaultAsync(x => x.Name.Equals(RoleConstants.StudentRoleName) && !x.IsDeleted).Result.Id;

            return await _entities
                .Include(user => user.UserRole)
                    .ThenInclude(userRole => userRole.Role)
                .Where(x =>x.UserRole.RoleId == studentRoleId && x.UserName.StartsWith(userNamePrefix))
                .ToListAsync();
        }

        public async Task<List<User>> GetAllByRoleName(string roleName)
        {
            return await _entities
                .Include(user => user.UserRole)
                    .ThenInclude(userRole => userRole.Role)
                .Where(x => x.UserRole.Role.Name == roleName).ToListAsync();
        }
    }
}
