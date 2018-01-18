using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Repository.Base;
using Data.Core.Domain;
using Data.Core.Interfaces;
using Data.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Business.Repository
{
    public class TestsRepository :GenericRepository<Test> ,ITestsRepository
    {
        public TestsRepository(DatabaseContext context) : base(context)
        {
        }

        public override async Task<Test> GetByIdAsync(Guid id)
        {
          return await _entities.Include(x => x.TestType).FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        } 

        public async Task<List<Test>> GetAllTestsOfTeacherAsync(Guid teacherId)
        {
            return await _context.Tests.Where(x => x.UserId == teacherId && !x.IsDeleted).ToListAsync();
        }
    }
}
