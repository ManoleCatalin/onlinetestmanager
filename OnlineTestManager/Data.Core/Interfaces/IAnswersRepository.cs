using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Core.Domain;

namespace Data.Core.Interfaces
{
    public interface IAnswersRepository
    {
        Task<List<Answer>> GetAnswersAsync();
        Task<Answer> GetAnswerByIdAsync(Guid id);
        Task<Answer> InsertAnswerAsync(Answer answer);
        Task<bool> UpdateAnswerAsync(Answer answer);
        Task<bool> DeleteAnswerAsync(Guid id);
    }
}