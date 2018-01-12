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
    public class TestInstancesRepository : GenericRepository<TestInstance>, ITestInstancesRepository
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

        public async Task<List<TestInstance>> GetAllTestInstancesOfStudentAsync(Guid studentId)
        {
            return await _context.TestInstances
                .Include(t => t.Group)
                    .ThenInclude(g => g.UserGroups)
                .Where(t => 0 != t.Group.UserGroups.Count(x => x.UserId == studentId))
                .ToListAsync();
        }

        public async Task<Exercise> GetNextExerciseAsync(Guid studentId, Guid testInstanceId)
        {
            var testsInstances = await GetAllTestInstancesOfStudentAsync(studentId);

            var testInstance = testsInstances.Find(x => x.Id == testInstanceId);
            if (null == testInstance)
            {
                return null;
            }

            var exercises = _context.Exercises.Include(x => x.ExerciseResponses)
                    .ThenInclude(x => x.MarkedAsCorrects)
                .Include(x => x.Answers)
                .Where(e => 0 == e.ExerciseResponses.Count(x => x.MarkedAsCorrects.Count == 0));

            return exercises.Any() ? exercises.First() : null;
        }

        public override async Task<TestInstance> InsertAsync(TestInstance testInstance)
        {
            _entities.Add(testInstance);
            await _context.SaveChangesAsync();

            return testInstance;
        }
    }
}
