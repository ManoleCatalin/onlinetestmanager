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
    public class AnswersRepository : GenericRepository<Answer>, IAnswersRepository
    {
        public AnswersRepository(DatabaseContext context) : base(context)
        {
        }

        public async Task<List<Answer>> GetAllAnswersOfExerciseAsync(Guid exerciseId)
        {
            return await _entities.Where(x => x.ExerciseId == exerciseId && !x.IsDeleted).ToListAsync();
        }

    }
}