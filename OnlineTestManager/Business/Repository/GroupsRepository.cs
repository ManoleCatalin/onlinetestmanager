using Business.Repository.Base;
using Data.Core.Domain;
using Data.Core.Interfaces;
using Data.Persistence;

namespace Business.Repository
{
    public class GroupsRepository : GenericRepository<Group>,IGroupsRepository
    {
        public GroupsRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
