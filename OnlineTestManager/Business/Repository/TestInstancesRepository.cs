using Business.Repository.Base;
using Data.Core.Domain;
using Data.Core.Interfaces;
using Data.Persistence;

namespace Business.Repository
{
    public class TestInstancesRepository :GenericRepository<TestInstance>, ITestInstancesRepository
    {
        public TestInstancesRepository(DatabaseContext context) : base(context)
        {
        }   
    }
}
