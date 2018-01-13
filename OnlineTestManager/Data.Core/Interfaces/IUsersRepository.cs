using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Core.Domain;

namespace Data.Core.Interfaces
{
    public interface IUsersRepository : IGenericRepository<User>
    {
        Task<List<User>> GetStudentsByNamePrefixAsync(string userNamePrefix);
        Task<List<User>> GetAllByRoleName(string roleName);
    }
}