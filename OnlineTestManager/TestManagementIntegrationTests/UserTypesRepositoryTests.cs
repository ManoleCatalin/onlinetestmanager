using Business.Repository;
using Data.Core.Domain;
using FluentAssertions;
using TestManagementIntegrationTests.Base;
using Xunit;

namespace TestManagementIntegrationTests
{
    public class UserTypesRepositoryTests : BaseIntegrationTest
    {
        [Fact]
        public void Given_UserTypes_When_GetUserTypesAsyncsIsCalled_Then_ShouldReturnZeroUserTypes()
        {
            RunOnDatabase(context =>
            {
                // ARRANGE 
                var userTypesRepository = new UserTypesRepository(context);

                // ACT
                var userTypes = userTypesRepository.GetUserTypesAsync();
                var counter = userTypes.Result.Count;
                // ASSERT
                counter.Should().Be(0);
            });
        }

        [Fact]
        public void Given_UserType_When_NewUsertTypeIsAdded_Then_ShouldHaveOneUserTypeInDatabase()
        {
            RunOnDatabase(context =>
            {
                // ARRANGE 
                var userTypesRepository = new UserTypesRepository(context);
                var userType = UserType.Create("student");
                var userTypeInserted = userTypesRepository.InsertUserTypeAsync(userType).Result;
                // ACT
                var result = userTypesRepository.GetUserTypeByIdAsync(userTypeInserted.Id);
                // ASSERT
                result.Should().NotBe(null);
            });
        }

        [Fact]
        public void Given_UserType_When_UpdateUserTypeAsync_Then_ShouldBeTrue()
        {
            RunOnDatabase(context =>
            {
                // ARRANGE 
                var userTypeRepository = new UserTypesRepository(context);
                var userType = UserType.Create("student");
                context.UserTypes.Add(userType);
                context.SaveChanges();
                // ACT
                userType.Update("teacher");
                var result = userTypeRepository.UpdateUserTypeAsync(userType);
                // ASSERT

                result.Result.Should().Be(true);
            });

        }

        [Fact]
        public void Given_UserType_When_DeleteUserTypeAsync_Then_ShouldBeTrue()
        {
            RunOnDatabase(context =>
            {
                // ARRANGE 
                var userTypesRepository = new UserTypesRepository(context);
                var userType = UserType.Create("student");
                context.UserTypes.Add(userType);
                context.SaveChanges();
                // ACT
                var result = userTypesRepository.DeleteUserTypeAsync(userType.Id);
                // ASSERT
                result.Result.Should().Be(true);
            });

        }
    }
}
