using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Core.Domain;

namespace Data.Core.Interfaces
{
    public interface IGradesRepository
    {
        Task<List<Grade>> GetGradesAsync();
        Task<Grade> GetGradeAsync(Guid userId, Guid testInstanceId);
        Task<Grade> InsertGradeAsync(Grade grade);
        Task<bool> UpdateGradeAsync(Grade grade);
        Task<bool> DeleteGradeAsync(Guid userId, Guid testInstanceId);
    }
}

