using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Core.Domain;

namespace Data.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> DeleteUserAsync(Guid id);
        Task<User> GetUserByIdAsync(Guid id);
        Task<List<User>> GetUsersAsync();
        Task<User> InsertUserAsync(User user);
        Task<bool> UpdateUserAsync(User user);
    }
}