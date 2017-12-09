using System;
using Data.Persistence;
using Microsoft.EntityFrameworkCore;
using Xunit;

[assembly: CollectionBehavior(DisableTestParallelization = true)]
namespace TestManagementIntegrationTests.Base
{
    public abstract class BaseIntegrationTest : IDisposable
    {
        public virtual bool UseSqlServer => true;

        public BaseIntegrationTest()
        {
            DestroyDatabase();
            CreateDatabase();
        }

        protected void RunOnDatabase(Action<DatabaseContext> databaseAction)
        {
            if (UseSqlServer)
            {
                RunOnSqlServer(databaseAction);
            }
            else
            {
                RunOnMemory(databaseAction);
            }
        }

        private void RunOnMemory(Action<DatabaseContext> databaseAction)
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase("TodosList")
                .Options;

            using (var context = new DatabaseContext(options))
            {
                databaseAction(context);
            }
        }

        private void RunOnSqlServer(Action<DatabaseContext> databaseAction)
        {
            var connection = @"Server = .\SQLEXPRESS; Database = TestManagement.Development; Trusted_Connection = true; MultipleActiveResultSets=true";
            
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseSqlServer(connection)
                .Options;

            using (var context = new DatabaseContext(options))
            {
                databaseAction(context);
            }
        }

        private void CreateDatabase()
        {
            RunOnDatabase(ctx => ctx.Database.EnsureCreated());
        }

        private void DestroyDatabase()
        {
            RunOnDatabase(ctx => ctx.Database.EnsureDeleted());
        }

        public void Dispose()
        {
          DestroyDatabase();
        }
    }
}