using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Core.Domain;

namespace Data.Core.Interfaces
{
    public interface IUserGroupsRepository
    {
        Task<List<UserGroup>> GetUserGroupsAsync();
        Task<UserGroup> GetUserGroupAsync(Guid userId, Guid groupId);
        Task<UserGroup> InsertUserGroupAsync(UserGroup group);
        Task<bool> DeleteUserGroupAsync(Guid userId, Guid groupId);
    }
}
