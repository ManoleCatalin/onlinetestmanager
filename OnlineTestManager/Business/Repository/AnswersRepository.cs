using Business.Repository.Base;
using Data.Core.Domain;
using Data.Core.Interfaces;
using Data.Persistence;

namespace Business.Repository
{
    public class AnswersRepository : GenericRepository<Answer>, IAnswersRepository
    {
        public AnswersRepository(DatabaseContext context) : base(context)
        {
        }
    }
}