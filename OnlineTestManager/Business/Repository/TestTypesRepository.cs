using Business.Repository.Base;
using Data.Core.Domain;
using Data.Core.Interfaces;
using Data.Persistence;

namespace Business.Repository
{
    public class TestTypesRepository : GenericRepository<TestType>, ITestTypesRepository
    {
        public TestTypesRepository(DatabaseContext context) : base(context)
        { 
        }
    }
}
