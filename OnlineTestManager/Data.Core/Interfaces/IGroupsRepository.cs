using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Core.Domain;

namespace Data.Core.Interfaces
{
    public interface IGroupsRepository
    {
        Task<List<Group>> GetGroupsAsync();
        Task<Group> GetGroupByIdAsync(Guid id);
        Task<Group> InsertGroupAsync(Group caseGroup);
        Task<bool> UpdateGroupAsync(Group caseGroup);
        Task<bool> DeleteGroupAsync(Guid id);
    }
}
