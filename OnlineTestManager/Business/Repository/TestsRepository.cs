using Business.Repository.Base;
using Data.Core.Domain;
using Data.Core.Interfaces;
using Data.Persistence;

namespace Business.Repository
{
    public class TestsRepository :GenericRepository<Test> ,ITestsRepository
    {
        public TestsRepository(DatabaseContext context) : base(context)
        {
        }    
    }
}
