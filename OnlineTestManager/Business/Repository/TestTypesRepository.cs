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
    public class TestTypesRepository: ITestTypesRepository
    {

        private readonly DatabaseContext _context;

        public TestTypesRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<TestType>> GetTestTypesAsync()
        {
            return await _context.TestTypes.ToListAsync();
        }

        public async Task<TestType> GetTestTypeByIdAsync(Guid id)
        {
            return await _context.TestTypes.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<TestType> InsertTestTypeAsync(TestType testType)
        {
            _context.TestTypes.Add(testType);
            await _context.SaveChangesAsync();
            return testType;
        }

        public async Task<bool> UpdateTestTypeAsync(TestType testType)
        {
            _context.TestTypes.Update(testType);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteTestTypeAsync(Guid id)
        {
            _context.TestTypes.Remove(_context.TestTypes.FirstOrDefault(t => t.Id == id));
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
