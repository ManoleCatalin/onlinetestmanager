using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Core.Domain;

namespace Data.Core.Interfaces
{
    public interface IExercisesRepository : IGenericRepository<Exercise>
    {
        Task<List<Exercise>> GetAllExercisesOfTestAsync(Guid testId);
    }
}