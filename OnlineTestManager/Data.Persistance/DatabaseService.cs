using Microsoft.EntityFrameworkCore;

namespace Data.Persistence
{
    public class DatabaseService : DbContext
    {
        public DatabaseService(DbContextOptions<DatabaseService> options) : base(options)
        {
        }
    }
}
