using System.Linq;
using Business.Repository;
using Data.Core.Domain;
using FluentAssertions;
using TestManagementIntegrationTests.Base;
using Xunit;

namespace TestManagementIntegrationTests
{
    public class UsersRepositoryTests : BaseIntegrationTest
    {
        [Fact]
        public void Given_Users_When_GetUsersAsyncsIsCalled_Then_ShouldReturnZeroUsers()
        {
            RunOnDatabase(context => {
                // ARRANGE 
                var userRepository = new UsersRepository(context);

                // ACT
                var users = userRepository.GetAllAsync();
                var counter = users.Result.Count;
                // ASSERT
                counter.Should().Be(0);
            });
        }

        [Fact]
        public void Given_User_When_NewUserIsAdded_Then_ShouldHaveOneUserInDatabase()
        {
            RunOnDatabase(  context => {
                // ARRANGE 
                

                context.UserTypes.Add(UserType.Create("student"));
                context.SaveChanges();

                var userType = context.UserTypes.ToList().FirstOrDefault();
                var userRepository = new UsersRepository(context);
                if (userType == null) return;
                var user = User.Create(
                
                  
                    "User1",
                    "User last name",
                    "test@test.ro",
                    "parola",
                    userType.Id

                );
                var userInserted = userRepository.InsertAsync(user).Result;
                // ACT
                var result =userRepository.GetByIdAsync(userInserted.Id);
                // ASSERT
                result.Should().NotBe(null);
            });

        }
        [Fact]
        public void Given_User_When_UpdateUserAsync_Then_ShouldBeTrue()
        {
            RunOnDatabase(context => {
                // ARRANGE 


                context.UserTypes.Add(UserType.Create("student"));
                context.SaveChanges();

                var userType = context.UserTypes.ToList().FirstOrDefault();
                var userRepository = new UsersRepository(context);
                if (userType == null) return;
                var user = User.Create(


                    "User1",
                    "User last name",
                    "test@test.ro",
                    "parola",
                    userType.Id

                );
                context.Add(user);
                context.SaveChanges();
                user.Update("Ion", "User last name", "test@test.ro", "parola",userType.Id);
              

                // ACT
                var result = userRepository.UpdateAsync(user); 
                // ASSERT
                result.Result.Should().Be(true);
            });

        }

        [Fact]
        public void Given_User_When_DeleteUserAsync_Then_ShouldBeTrue()
        {
            RunOnDatabase(context => {
                // ARRANGE 


                context.UserTypes.Add(UserType.Create("student"));
                context.SaveChanges();

                var userType = context.UserTypes.ToList().FirstOrDefault();
                var userRepository = new UsersRepository(context);
                if (userType == null) return;
                var user = User.Create(


                    "User1",
                    "User last name",
                    "test@test.ro",
                    "parola",
                    userType.Id

                );
                context.Add(user);
                context.SaveChanges();
                


                // ACT
                var result = userRepository.DeleteAsync(user.Id);
                // ASSERT
                result.Result.Should().Be(true);
            });

        }
    }
}
