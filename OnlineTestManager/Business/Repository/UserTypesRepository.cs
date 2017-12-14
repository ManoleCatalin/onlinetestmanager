using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Core.Domain;
using Data.Core.Interfaces;
using Data.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Business.Repository
{
    public class UserTypesRepository :GenericRepository<UserType>, IUserTypesRepository
    {
        private readonly DatabaseContext _context;

        public UserTypesRepository(DatabaseContext context):base(context)
        {
            _context = context;
        }

      
    }
}
