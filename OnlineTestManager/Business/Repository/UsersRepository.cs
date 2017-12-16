using Business.Repository.Base;
using Data.Core.Domain;
using Data.Core.Interfaces;
using Data.Persistence;

namespace Business.Repository
{
    public class UsersRepository : GenericRepository<User>, IUsersRepository
    {
        public UsersRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
