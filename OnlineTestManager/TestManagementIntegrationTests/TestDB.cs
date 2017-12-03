using System.Diagnostics;
using System.Linq;
using Data.Core.Domain;
using Data.Persistence;
using FluentAssertions;
using TestManagementIntegrationTests.Base;
using Xunit;

namespace TestManagementIntegrationTests
{
    public class TestDb : BaseIntegrationTest
    {
        [Fact]
        public void Test1()
        {
            RunOnDatabase(ctx =>
            {
                //Arrange
                DatabaseContext databaseContext = ctx;

                databaseContext.UserTypes.Add(UserType.Create("student"));
                databaseContext.SaveChanges();

                var userType = databaseContext.UserTypes.ToList().FirstOrDefault();
                if (userType == null)
                {
                    false.Should().Be(true);
                }

                Debug.Assert(userType != null, nameof(userType) + " != null");
                databaseContext.Users.Add(User.Create("Johny", "Bravo", "johnnybravo@gmail.com", "#$$RR#$TED",
                    userType.Id));

                databaseContext.SaveChanges();

                //Act
                var users = databaseContext.Users.ToList();


                //Assert
                Assert.Single(users);

                var user = users.First();

                databaseContext.Groups.Add(Group.Create("A2", "Grupa A2", user.Id));
                databaseContext.SaveChanges();

                Assert.Single(databaseContext.Groups);
                var group = databaseContext.Groups.First();


                databaseContext.UserGroups.Add(UserGroup.Create(user.Id, group.Id));
                databaseContext.SaveChanges();

                Assert.Single(databaseContext.UserGroups);
            });
        }
    }
}
