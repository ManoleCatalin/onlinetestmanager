using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Core.Domain;

namespace Data.Core.Interfaces
{
    public interface IFilesRepository
    {
        Task<List<File>> GetFilesAsync();
        Task<File> GetFileByIdAsync(Guid id);

        Task<File> InsertFileAsync(File file);
        Task<bool> UpdateFileAsync(File file);
        Task<bool> DeleteFileAsync(Guid id);
    }
}