using Data.Core.Domain;
using Data.Core.Interfaces;
using Data.Persistence;

namespace Business.Repository
{
    public class FilesRepository :GenericRepository<File>, IFilesRepository
    {
        private readonly DatabaseContext _context;

        public FilesRepository(DatabaseContext context) : base(context)
        {
            _context = context;
        }

        
    }
}
