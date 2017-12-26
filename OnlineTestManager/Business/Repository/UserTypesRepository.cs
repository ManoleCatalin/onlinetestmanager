using System.Threading.Tasks;
using Business.Repository.Base;
using Data.Core.Domain;
using Data.Core.Interfaces;
using Data.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Business.Repository
{
    public class UserTypesRepository : GenericRepository<UserType>, IUserTypesRepository
    {
        public UserTypesRepository(DatabaseContext context): base(context)
        {
        }

        public async Task<UserType> GetByNameAsync(string name) => await _entities.FirstOrDefaultAsync(x => x.Name.Equals(name));
    }
}
