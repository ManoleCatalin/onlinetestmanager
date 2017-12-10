using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Core.Domain;
using Data.Core.Interfaces;
using Data.Persistence;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Business.Repository
{
    public class TestsRepository : ITestsRepository
    {

        private readonly DatabaseContext _context;

        public TestsRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<Test>> GetTestsAsync()
        {
            return await _context.Tests.ToListAsync();
        }

        public async Task<Test> GetTestByIdAsync(Guid id)
        {
            return await _context.Tests.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Test> InsertTestAsync(Test test)
        {
            _context.Tests.Add(test);
            await _context.SaveChangesAsync();
            return test;
        }

        public async Task<bool> UpdateTestAsync(Test test)
        {
            _context.Tests.Update(test);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteTestAsync(Guid id)
        {
            _context.Tests.Remove(_context.Tests.FirstOrDefault(t => t.Id == id));
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
