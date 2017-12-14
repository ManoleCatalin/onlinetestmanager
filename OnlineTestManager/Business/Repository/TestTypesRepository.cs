using Data.Core.Domain;
using Data.Core.Interfaces;
using Data.Persistence;

namespace Business.Repository
{
    public class TestTypesRepository:GenericRepository<TestType>, ITestTypesRepository
    {

        private readonly DatabaseContext _context;

        public TestTypesRepository(DatabaseContext context):base(context)
        {
            _context = context;
        }

       
    }
}
