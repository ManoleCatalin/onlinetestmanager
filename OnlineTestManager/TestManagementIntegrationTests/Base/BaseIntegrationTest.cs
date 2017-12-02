using System;
using Data.Persistence;
using Microsoft.EntityFrameworkCore;

namespace TestManagementIntegrationTests.Base
{
    public abstract class BaseIntegrationTest : IDisposable
    {
        public virtual bool UseSqlServer => false;

        public BaseIntegrationTest()
        {
            DestroyDatabase();
            CreateDatabase();
        }

        protected void RunOnDatabase(Action<DatabaseService> databaseAction)
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

        private void RunOnMemory(Action<DatabaseService> databaseAction)
        {
            var options = new DbContextOptionsBuilder<DatabaseService>()
                .UseInMemoryDatabase("TodosList")
                .Options;

            using (var context = new DatabaseService(options))
            {
                databaseAction(context);
            }
        }

        private void RunOnSqlServer(Action<DatabaseService> databaseAction)
        {
            var connection = @"Server = .\SQLEXPRESS; Database = TestManagement.Development.Tests; Trusted_Connection = true;";

            var options = new DbContextOptionsBuilder<DatabaseService>()
                .UseSqlServer(connection)
                .Options;

            using (var context = new DatabaseService(options))
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