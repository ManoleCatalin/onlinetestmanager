using System.Linq;
using Business.Repository;
using Data.Core.Domain;
using FluentAssertions;
using TestManagementIntegrationTests.Base;
using Xunit;

namespace TestManagementIntegrationTests
{
    public class GroupsRepositoryTests : BaseIntegrationTest
    {
        [Fact]
        public void Given_Groups_When_GetGroupsAsyncsIsCalled_Then_ShouldReturnZeroGroups()
        {
            RunOnDatabase(context => {
                // ARRANGE 
                var groupRepository = new GroupsRepository(context);

                // ACT
                var groups = groupRepository.GetAllAsync();
                var counter = groups.Result.Count;
                // ASSERT
                counter.Should().Be(0);
            });
        }

        [Fact]
        public void Given_Group_When_NewGroupIsAdded_Then_ShouldHaveOneGroupInDatabase()
        {
            RunOnDatabase(context => {
                // ARRANGE 


                context.Roles.Add(Role.Create("student"));
                context.SaveChanges();

                var userType = context.Roles.FirstOrDefault();
                if (userType != null)
                {
                    var user = User.Create(
                        "User first name",
                        "User last name",
                        "User",
                        "test@test.ro",
                        "parola"
                    );
                    context.Users.Add(user);
                }
                context.SaveChanges();

                var userId = context.Users.FirstOrDefault();

                var groupRepository = new GroupsRepository(context);
                if (userId == null) return;
                var group = Group.Create(
                    
                    "A2",
                    "grupa din anul III",
                    userId.Id
                
                );
                var groupInserted = groupRepository.InsertAsync(@group).Result;
                // ACT
                var result = groupRepository.GetByIdAsync(groupInserted.Id);
                // ASSERT
                result.Should().NotBe(null);
            });

        }

        [Fact]
        public void Given_Group_When_UpdateGroupAsync_Then_ShouldBeTrue()
        {
            RunOnDatabase(context => {
                // ARRANGE 


                context.Roles.Add(Role.Create("student"));
                context.SaveChanges();

                var userType = context.Roles.FirstOrDefault();
                if (userType != null)
                {
                    var user = User.Create(
                        "User first name",
                        "User last name",
                        "User",
                        "test@test.ro",
                        "parola"
                    );
                    context.Users.Add(user);
                }
                context.SaveChanges();

                var userId = context.Users.FirstOrDefault();

                var groupRepository = new GroupsRepository(context);
                if (userId == null) return;
                var group = Group.Create(

                    "A2",
                    "grupa din anul III",
                    userId.Id

                );

                context.Add(@group);
                context.SaveChanges();

                @group.Update("A3", "grupa din anul I", userId.Id, false);


                // ACT
                var result = groupRepository.UpdateAsync(@group);
                // ASSERT
                result.Result.Should().Be(true);
            });

        }

        [Fact]
        public void Given_Group_When_DeleteGroupAsync_Then_ShouldBeTrue()
        {
            RunOnDatabase(context =>
            {
                // ARRANGE 


                context.Roles.Add(Role.Create("student"));
                context.SaveChanges();

                var userType = context.Roles.FirstOrDefault();
                if (userType != null)
                {
                    var user = User.Create(
                        "User first name",
                        "User last name",
                        "User",
                        "test@test.ro",
                        "parola"
                    );
                    context.Users.Add(user);
                }
                context.SaveChanges();

                var userId = context.Users.FirstOrDefault();

                var groupRepository = new GroupsRepository(context);
                if (userId == null) return;
                var group = Group.Create(

                    "A2",
                    "grupa din anul III",
                    userId.Id

                );

                context.Add(@group);
                context.SaveChanges();
        
                // ACT
                var result = groupRepository.DeleteAsync(@group.Id);
                // ASSERT
                result.Result.Should().Be(true);
            });

        }
    }
}

