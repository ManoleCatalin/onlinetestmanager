using Data.Core.Domain;
using Data.Core.Interfaces;
using Data.Persistence;

namespace Business.Repository
{
    public class AnswersRepository : GenericRepository<Answer>, IAnswersRepository
    {
        private readonly DatabaseContext _context;

        public AnswersRepository(DatabaseContext context) : base(context)
        {
            _context = context;
        }
        
       

    }
}
