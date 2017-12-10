using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Data.Core.Domain;

namespace Data.Core.Interfaces
{
    public interface ITestInstancesRepository
    {
        Task<List<TestInstance>> GetTestInstancesAsync();
        Task<TestInstance> GetTestInstanceByIdAsync(Guid id);
        Task<TestInstance> InsertTestInstanceAsync(TestInstance testInstance);
        Task<bool> UpdateTestInstanceAsync(TestInstance testInstance);
        Task<bool> DeleteTestInstanceAsync(Guid id);
    }
}
