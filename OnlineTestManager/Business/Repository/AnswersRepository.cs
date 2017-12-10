using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Core.Domain;
using Data.Core.Interfaces;
using Data.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Business.Repository
{
    public class AnswersRepository : IAnswersRepository
    {
        private readonly DatabaseContext _context;

        public AnswersRepository(DatabaseContext context) => _context = context;

        public async Task<List<Answer>> GetAnswersAsync() => await _context.Answers.ToListAsync();

        public async Task<Answer> GetAnswerByIdAsync(Guid id) => await _context.Answers.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<Answer> InsertAnswerAsync(Answer answer)
        {
            _context.Answers.Add(answer);
            await _context.SaveChangesAsync();
            return answer;
        }

        public async Task<bool> UpdateAnswerAsync(Answer answer)
        {
            _context.Answers.Update(answer);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAnswerAsync(Guid id)
        {
            _context.Answers.Remove(_context.Answers.FirstOrDefault(u => u.Id == id));
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
