using Data.Core.Domain;
using Data.Core.Interfaces;
using Data.Persistence;

namespace Business.Repository
{
    public class GroupsRepository : GenericRepository<Group>,IGroupsRepository
    {
        private readonly DatabaseContext _context;

        public GroupsRepository(DatabaseContext context):base(context)
        {
            _context = context;
        }

       
    }
}
