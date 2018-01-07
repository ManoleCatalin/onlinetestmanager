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
    public class TestInstancesRepository :GenericRepository<TestInstance>, ITestInstancesRepository
    {
        public TestInstancesRepository(DatabaseContext context) : base(context)
        {
        }   

        public async Task<List<TestInstance>> GetAllTestInstancesOfTeacherAsync(Guid teacherId)
        {
            return await _entities
                .Include(s => s.Test)
                .Where(s => s.Test.UserId == teacherId).ToListAsync();
        }
    }

}
