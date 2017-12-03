using System.Linq;
using Data.Core.Domain;
using Data.Persistence;
using TestManagementIntegrationTests.Base;
using Xunit;

namespace TestManagementIntegrationTests
{
    public class test : BaseIntegrationTest
    {
        [Fact]
        public void Test1()
        {
            RunOnDatabase(ctx => {
                //Arrange
                DatabaseContext ds = ctx;
                ds.Users.Add(new User());
                ds.SaveChanges();

                //Act
                var count = ds.Users.ToList().Count;

                //Assert
                Assert.Equal(1, count);
            });
        }
    }
}
