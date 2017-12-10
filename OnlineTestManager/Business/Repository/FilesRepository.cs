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
    public class FilesRepository : IFilesRepository
    {
        private readonly DatabaseContext _context;

        public FilesRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<File>> GetFilesAsync()
        {
            return await _context.Files.ToListAsync();
        }

        public Task<File> GetFileByIdAsync(Guid id)
        {
            return _context.Files.FirstOrDefaultAsync(file => file.Id == id);
        }

        public async Task<File> InsertFileAsync(File file)
        {
            _context.Files.Add(file);
            await _context.SaveChangesAsync();
            return file;
        }

        public async Task<bool> UpdateFileAsync(File file)
        {
            _context.Files.Update(file);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteFileAsync(Guid id)
        {
            _context.Files.Remove(_context.Files.FirstOrDefault(file => file.Id == id));
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
