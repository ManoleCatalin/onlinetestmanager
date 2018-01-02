using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Core.Domain;

namespace Data.Core.Interfaces
{
    public interface ITestsRepository : IGenericRepository<Test>
    {
        Task<List<Test>>  GetAllTestsOfTeacherAsync(Guid teacherId);
    }
}
