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
    public class ExercisesRepository : IExercisesRepository
    {
        private readonly DatabaseContext _context;

        public ExercisesRepository(DatabaseContext context) => _context = context;

        public async Task<List<Exercise>> GetExerciseAsync() => await _context.Exercises.ToListAsync();

        public async Task<Exercise> GetExerciseByIdAsync(Guid id) => await _context.Exercises.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<Exercise> InsertExerciseAsync(Exercise exercise)
        {
            _context.Exercises.Add(exercise);
            await _context.SaveChangesAsync();
            return exercise;
        }

        public async Task<bool> UpdateExerciseAsync(Exercise exercise)
        {
            _context.Exercises.Update(exercise);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteExerciseAsync(Guid id)
        {
            _context.Exercises.Remove(_context.Exercises.FirstOrDefault(u => u.Id == id));
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
