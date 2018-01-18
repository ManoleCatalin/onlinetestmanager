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
    public class GroupsRepository : GenericRepository<Group>, IGroupsRepository
    {
     
        public GroupsRepository(DatabaseContext context) : base(context)
        {
        
        }
        public override async Task<Group> GetByIdAsync(Guid id)
        {
             Group groups = await _entities
                .Where(b => b.IsDeleted == false)
                .Include(s => s.UserGroups)
                .ThenInclude(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == id );

            if (groups == null)
                return null;
            List<UserGroup> listGroup = groups.UserGroups.ToList();
            listGroup.RemoveAll(x => x.IsDeleted);
            groups.UserGroups = listGroup;

            return groups;
        }
     

        public async Task<bool> InsertStudentAsync(Guid groupId, Guid studentId)
        {
            
            User user = await _context.Users.FirstOrDefaultAsync(u => u.Id == studentId);
            if (user.Id != studentId)
            {
                return false;
            }

            Role studentRole = await _context.Roles.FirstOrDefaultAsync(role => role.Name.Equals(RoleConstants.StudentRoleName));

            UserRole isStudent = await _context.UserRoles.FirstOrDefaultAsync( ur => ur.UserId == studentId && ur.RoleId == studentRole.Id);

            if (isStudent.UserId != studentId)
            {
                return false;
            }
            var group = await _context.UserGroups.FirstOrDefaultAsync((g => g.GroupId.Equals(groupId)  && g.UserId.Equals(studentId)));
            if (@group != null && @group.IsDeleted)
            {
                group.IsDeleted = false;
                _context.UserGroups.Update(group);
                return await _context.SaveChangesAsync() > 0;
            }
            else
            {
                _context.UserGroups.Add(UserGroup.Create(studentId, groupId));
                return await _context.SaveChangesAsync() > 0;
            }
            
           
            
        }
       

        public async Task<bool> DeleteStudentAsync(Guid groupId, Guid studentId)
        {
            UserGroup entity=_context.UserGroups
                .FirstOrDefault(ug => ug.UserId == studentId && ug.GroupId == groupId);
            if (entity == null) return await _context.SaveChangesAsync() > 0;
            entity.IsDeleted = true;
            _context.UserGroups.Update(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<Group>> GetAllGroupsOfTeacherAsync(Guid teacherId)
        {
            return await _entities
                .Include(s => s.UserGroups)
                .ThenInclude(x => x.User)
                .Where(x => x.UserId == teacherId && !x.IsDeleted).ToListAsync();
        }
        
    }
}
