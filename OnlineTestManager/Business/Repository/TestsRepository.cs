using Data.Core.Domain;
using Data.Core.Interfaces;
using Data.Persistence;

namespace Business.Repository
{
    public class TestsRepository :GenericRepository<Test> ,ITestsRepository
    {

        private readonly DatabaseContext _context;

        public TestsRepository(DatabaseContext context):base(context)
        {
            _context = context;
        }

       
    }
}
