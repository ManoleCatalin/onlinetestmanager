using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Core.Domain;

namespace Data.Core.Interfaces
{
    public interface IExercisesRepository
    {
        Task<List<Exercise>> GetExerciseAsync();
        Task<Exercise> GetExerciseByIdAsync(Guid id);
        Task<Exercise> InsertExerciseAsync(Exercise exercise);
        Task<bool> UpdateExerciseAsync(Exercise exercise);
        Task<bool> DeleteExerciseAsync(Guid id);
    }
}