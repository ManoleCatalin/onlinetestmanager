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
                .Where(s => s.Test.UserId == teacherId && !s.IsDeleted).ToListAsync();
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
                .Include(t => t.Test)
                .ThenInclude(ti => ti.TestInstances)
                .Where(t => 0 != t.Test.TestInstances.Count(k => k.Id == testInstanceId));
            var exercisesNotAnsweredByThisUser = exercises
                .Where(e => 0 == e.ExerciseResponses.Count(x => e.Id == x.ExerciseId && x.UserId == studentId));

            return exercisesNotAnsweredByThisUser.Any() ? exercisesNotAnsweredByThisUser.First() : null;
        }

        public override async Task<TestInstance> InsertAsync(TestInstance testInstance)
        {
            _entities.Add(testInstance);
            await _context.SaveChangesAsync();

            return testInstance;
        }

        public async Task<ExerciseResponse> InsertExerciseResponseAsync(ExerciseResponse exerciseResponse)
        {
            var markedAsCorrects = exerciseResponse.MarkedAsCorrects;
            exerciseResponse.MarkedAsCorrects = null;
            _context.ExerciseResponses.Add(exerciseResponse);
            await _context.SaveChangesAsync();
            foreach (var item in markedAsCorrects)
            {
                _context.MarkedAsCorrects.Add(item);
            }
            await _context.SaveChangesAsync();
            return exerciseResponse;
        }

    }
}
