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
    public class TestInstancesRepository : ITestInstancesRepository
    {
        private readonly DatabaseContext _context;

        public TestInstancesRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<TestInstance>> GetTestInstancesAsync()
        {
            return await _context.TestInstances.ToListAsync();
        }

        public async Task<TestInstance> GetTestInstanceByIdAsync(Guid id)
        {
            return await _context.TestInstances.FirstOrDefaultAsync(ti => ti.Id == id);
        }

        public async Task<TestInstance> InsertTestInstanceAsync(TestInstance testInstance)
        {
            _context.TestInstances.Add(testInstance);
            await _context.SaveChangesAsync();
            return testInstance;
        }

        public async Task<bool> UpdateTestInstanceAsync(TestInstance testInstance)
        {
            _context.TestInstances.Update(testInstance);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteTestInstanceAsync(Guid id)
        {
            _context.TestInstances.Remove(_context.TestInstances.FirstOrDefault(ti => ti.Id == id));
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
