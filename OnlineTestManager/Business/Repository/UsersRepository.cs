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
            return await _entities.Include(user => user.UserRole).ToListAsync();
        }

        public override async Task<User> GetByIdAsync(Guid id)
        {
            return await _entities
                .Include(user => user.UserRole)
                    .ThenInclude(userRole => userRole.Role)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User> GetByUserNameAsync(string userName)
        {
            return await _entities
                .Include(user => user.UserRole)
                    .ThenInclude(userRole => userRole.Role)
                .FirstOrDefaultAsync(x => x.UserName == userName);
        }

        //public async Task<User> InsertAsync(User user, string roleName)
        //{
        //    _entities.Add(user);
        //    await _context.SaveChangesAsync();

        //    var insertedUser = GetByUserNameAsync(user.UserName).Result;
        //    var roleId = _context.Roles.FirstOrDefaultAsync(x=> x.Name.Equals(roleName)).Result.Id;

        //    var userRole = new UserRole{UserId = user.Id, RoleId = roleId};
        //    _context.UserRoles.Add(userRole);

        //    await _context.SaveChangesAsync();

        //    return user;
        //}
    }
}
