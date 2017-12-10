using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Core.Domain;

namespace Data.Core.Interfaces
{
    public interface IUserTypesRepository
    {
        Task<List<UserType>> GetUserTypesAsync();
        Task<UserType> GetUserTypeByIdAsync(Guid id);
        Task<UserType> InsertUserTypeAsync(UserType userType);
        Task<bool> UpdateUserTypeAsync(UserType userType);
        Task<bool> DeleteUserTypeAsync(Guid id);
    }
}