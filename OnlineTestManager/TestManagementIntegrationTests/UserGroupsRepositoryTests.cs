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


                context.UserTypes.Add(UserType.Create("student"));
                context.SaveChanges();

                var userType = context.UserTypes.ToList().FirstOrDefault();
                context.Users.Add(User.Create("User1", "User last name", "test@test.ro", "parola", userType.Id));
                context.SaveChanges();

                var user = context.Users.ToList().FirstOrDefault();
                context.Groups.Add(Group.Create("Grup", "Grup mare", user.Id));
                context.SaveChanges();
                var group = context.Groups.ToList().FirstOrDefault();

                var userGroupRepository = new UserGroupsRepository(context);
                var userGroup = UserGroup.Create(user.Id, group.Id);
                var insertedUserGroup = userGroupRepository.InsertUserGroupAsync(userGroup).Result;
                // ACT
                var result = userGroupRepository.GetUserGroupAsync(insertedUserGroup.UserId, insertedUserGroup.GroupId);
                // ASSERT
                result.Should().NotBe(null);
            });

        }

        [Fact]
        public void Given_UserGroup_When_DeleteUserGroupAsync_Then_ShouldBeTrue()
        {
            RunOnDatabase(context => {
                // ARRANGE 

                context.UserTypes.Add(UserType.Create("student"));
                context.SaveChanges();

                var userType = context.UserTypes.ToList().FirstOrDefault();
                context.Users.Add(User.Create("User1", "User last name", "test@test.ro", "parola", userType.Id));
                context.SaveChanges();

                var user = context.Users.ToList().FirstOrDefault();
                context.Groups.Add(Group.Create("Grup", "Grup mare", user.Id));
                context.SaveChanges();
                var group = context.Groups.ToList().FirstOrDefault();

                var userGroupRepository = new UserGroupsRepository(context);
                var userGroup = UserGroup.Create(user.Id, group.Id);

                context.Add(userGroup);
                context.SaveChanges();


                // ACT
                var result = userGroupRepository.DeleteUserGroupAsync(user.Id, group.Id);
                // ASSERT
                result.Result.Should().Be(true);
            });

        }
    }
}
