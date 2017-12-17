using Business.Repository.Base;
using Data.Core.Domain;
using Data.Core.Interfaces;
using Data.Persistence;

namespace Business.Repository
{
    public class FilesRepository :GenericRepository<File>, IFilesRepository
    {
        public FilesRepository(DatabaseContext context) : base(context)
        {
        }      
    }
}
