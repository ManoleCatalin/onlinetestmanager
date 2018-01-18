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
    public class UserGroupsRepository : IUserGroupsRepository
    {
        private readonly DatabaseContext _context;

        public UserGroupsRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<UserGroup>> GetUserGroupsAsync()
        {
            return await _context.UserGroups.Where(x=>!x.IsDeleted).ToListAsync();
        }

        public async Task<UserGroup> GetUserGroupAsync(Guid userId, Guid groupId)
        {
            return await _context.UserGroups.FirstOrDefaultAsync(x => x.UserId == userId && x.GroupId == groupId && !x.IsDeleted);
        }

        public async Task<UserGroup> InsertUserGroupAsync(UserGroup group)
        {
            _context.UserGroups.Add(group);
            await _context.SaveChangesAsync();
            return group;
        }
      

        public async Task<bool> DeleteUserGroupAsync(Guid userId, Guid groupId)
        {
           var entity=(_context.UserGroups.FirstOrDefault(x => x.UserId == userId && x.GroupId == groupId));
            if (entity == null) return await _context.SaveChangesAsync() > 0;
            entity.IsDeleted = true;
            _context.UserGroups.Update(entity);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
