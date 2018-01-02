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
    public class ExercisesRepository : GenericRepository<Exercise>, IExercisesRepository
    {
        public ExercisesRepository(DatabaseContext context) : base(context)
        {
        }

        public async Task<List<Exercise>> GetAllExercisesOfTestAsync(Guid testId)
        {
            return await _entities.Where(x => x.TestId == testId).ToListAsync();
        }
    }
}
