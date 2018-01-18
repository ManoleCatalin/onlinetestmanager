using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Core.Domain;

namespace Data.Core.Interfaces
{
    public interface IAnswersRepository : IGenericRepository<Answer>
    {
         Task<List<Answer>> GetAllAnswersOfExerciseAsync(Guid exerciseId);
         Task<Answer> GetCorrectAnswerOfExerciseAsync(Guid exerciseId);
    }
}