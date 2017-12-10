using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Data.Core.Domain;

namespace Data.Core.Interfaces
{
    public interface ITestsRepository
    {
        Task<List<Test>> GetTestsAsync();
        Task<Test> GetTestByIdAsync(Guid id);
        Task<Test> InsertTestAsync(Test renameMeWithLowerCaseTest);
        Task<bool> UpdateTestAsync(Test renameMeWithLowerCaseTest);
        Task<bool> DeleteTestAsync(Guid id);
    }
}
