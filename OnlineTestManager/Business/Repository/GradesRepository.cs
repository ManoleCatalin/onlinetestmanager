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
    public class GradesRepository : IGradesRepository
    {
        private readonly DatabaseContext _context;

        public GradesRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<Grade>> GetGradesAsync()
        {
            return await _context.Grades.ToListAsync();
        }

        public async Task<Grade> GetGradeAsync(Guid userId, Guid testInstanceId)
        {
            return await _context.Grades.FirstOrDefaultAsync(x => x.UserId == userId && x.TestInstanceId == testInstanceId);
        }

        public async Task<Grade> InsertGradeAsync(Grade grade)
        {
            _context.Grades.Add(grade);
            await _context.SaveChangesAsync();
            return grade;
        }

        public async Task<bool> UpdateGradeAsync(Grade grade)
        {
            _context.Grades.Update(grade);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteGradeAsync(Guid userId, Guid testInstanceId)
        {
            _context.Grades.Remove(_context.Grades.FirstOrDefault(x => x.UserId == userId && x.TestInstanceId == testInstanceId));
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
