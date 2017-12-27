using System.Threading.Tasks;
using Business.Repository.Base;
using Data.Core.Domain;
using Data.Core.Interfaces;
using Data.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Business.Repository
{
    public class RolesRepository : GenericRepository<Role>, IRolesRepository
    {
        public RolesRepository(DatabaseContext context) : base(context)
        {
        }

        public async Task<Role> GetByNameAsync(string name) => await _entities.FirstOrDefaultAsync(x => x.Name.Equals(name));
    }
}
