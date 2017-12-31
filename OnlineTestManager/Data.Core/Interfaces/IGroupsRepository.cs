using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Core.Domain;

namespace Data.Core.Interfaces
{
    public interface IGroupsRepository : IGenericRepository<Group>
    {
        Task<bool> InsertStudentAsync(Guid groupId, Guid studentId);
        Task<bool> DeleteStudentAsync(Guid groupId, Guid studentId);
        Task<List<Group>> GetAllGroupsOfTeacherAsync(Guid teacherId);
    }
}
