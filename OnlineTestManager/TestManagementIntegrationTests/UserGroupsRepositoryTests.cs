using System.Linq;
using Business.Repository;
using Data.Core.Domain;
using FluentAssertions;
using TestManagementIntegrationTests.Base;
using Xunit;

namespace TestManagementIntegrationTests
{
    public class UserGroupsRepositoryTests : BaseIntegrationTest
    {
        [Fact]
        public void Given_UserGroups_When_GetUserGroupsAsyncIsCalled_Then_ShouldReturnZeroUsers()
        {
            RunOnDatabase(context =>
            {
                // ARRANGE 
                var userGroupsRepository = new UserGroupsRepository(context);

                // ACT
                var userGroups = userGroupsRepository.GetUserGroupsAsync();
                var counter = userGroups.Result.Count;
                // ASSERT
                counter.Should().Be(0);
            });
        }

        [Fact]
        public void Given_UserGroups_When_NewUserGroupIsAdded_Then_ShouldHaveOneUserGroupInDatabase()
        {
            RunOnDatabase(context => {
                // ARRANGE 


                context.Roles.Add(Role.Create("student"));
                context.SaveChanges();

                var userType = context.Roles.FirstOrDefault();
                if (userType != null)
                    context.Users.Add(User.Create("User first name", "User last name", "User1", "test@test.ro", "parola"));
                context.SaveChanges();

                var user = context.Users.FirstOrDefault();
                if (user == null) return;
                context.Groups.Add(Group.Create("Grup", "Grup mare", user.Id));
                context.SaveChanges();
                var group = context.Groups.FirstOrDefault();

                var userGroupRepository = new UserGroupsRepository(context);
                if (@group == null) return;
                var userGroup = UserGroup.Create(user.Id, @group.Id);
                var insertedUserGroup = userGroupRepository.InsertUserGroupAsync(userGroup).Result;
                // ACT
                var result =
                    userGroupRepository.GetUserGroupAsync(insertedUserGroup.UserId, insertedUserGroup.GroupId);
                // ASSERT
                result.Should().NotBe(null);
            });

        }

        [Fact]
        public void Given_UserGroup_When_DeleteUserGroupAsync_Then_ShouldBeTrue()
        {
            RunOnDatabase(context => {
                // ARRANGE 

                context.Roles.Add(Role.Create("student"));
                context.SaveChanges();

                var userType = context.Roles.FirstOrDefault();
                if (userType != null)
                    context.Users.Add(User.Create("User first name", "User last name", "User1", "test@test.ro", "parola"));
                context.SaveChanges();

                var user = context.Users.FirstOrDefault();
                if (user == null) return;
                context.Groups.Add(Group.Create("Grup", "Grup mare", user.Id));
                context.SaveChanges();
                var group = context.Groups.FirstOrDefault();

                var userGroupRepository = new UserGroupsRepository(context);
                if (@group != null)
                {
                    var userGroup = UserGroup.Create(user.Id, @group.Id);

                    context.Add(userGroup);
                }
                context.SaveChanges();


                // ACT
                if (@group == null) return;
                var result = userGroupRepository.DeleteUserGroupAsync(user.Id, @group.Id);
                // ASSERT
                result.Result.Should().Be(true);
            });

        }
    }
}
