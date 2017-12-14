using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Core.Domain;
using Data.Core.Interfaces;
using Data.Persistence;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Business.Repository
{
    public class TestsRepository :GenericRepository<Test> ,ITestsRepository
    {

        private readonly DatabaseContext _context;

        public TestsRepository(DatabaseContext context):base(context)
        {
            _context = context;
        }

       
    }
}
