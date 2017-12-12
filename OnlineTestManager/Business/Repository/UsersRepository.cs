using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Core.Domain;
using Data.Core.Interfaces;
using Data.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Business.Repository
{
    public class UsersRepository :GenericRepository<User>, IUsersRepository
    {
        private readonly DatabaseContext _context;

        public UsersRepository(DatabaseContext context):base(context)
        {
            _context = context;
        }

    }
}
