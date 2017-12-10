using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Core.Domain;

namespace Data.Core.Interfaces
{
    public interface ITestTypesRepository
    {
        Task<List<TestType>> GetTestTypesAsync();
        Task<TestType> GetTestTypeByIdAsync(Guid id);
        Task<TestType> InsertTestTypeAsync(TestType testType);
        Task<bool> UpdateTestTypeAsync(TestType testType);
        Task<bool> DeleteTestTypeAsync(Guid id);
    }
}
