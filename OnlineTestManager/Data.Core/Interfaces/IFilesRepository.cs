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

        Task<File> InsertFileAsync(File renameMeWithLowerCaseFile);
        Task<bool> UpdateFileAsync(File renameMeWithLowerCaseFile);
        Task<bool> DeleteFileAsync(Guid id);
    }
    
}