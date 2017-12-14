using Data.Core.Domain;
using Data.Core.Interfaces;
using Data.Persistence;

namespace Business.Repository
{
    public class TestInstancesRepository :GenericRepository<TestInstance>, ITestInstancesRepository
    {
        private readonly DatabaseContext _context;

        public TestInstancesRepository(DatabaseContext context):base(context)
        {
            _context = context;
        }

       
    }
}
