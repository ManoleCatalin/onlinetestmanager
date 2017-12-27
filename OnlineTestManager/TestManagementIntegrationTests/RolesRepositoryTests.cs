using Business.Repository;
using Data.Core.Domain;
using FluentAssertions;
using TestManagementIntegrationTests.Base;
using Xunit;

namespace TestManagementIntegrationTests
{
    public class RolesRepositoryTests : BaseIntegrationTest
    {
        [Fact]
        public void Given_UserTypes_When_GetUserTypesAsyncsIsCalled_Then_ShouldReturnZeroUserTypes()
        {
            RunOnDatabase(context =>
            {
                // ARRANGE 
                var userTypesRepository = new RolesRepository(context);

                // ACT
                var userTypes = userTypesRepository.GetAllAsync();
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
                var userTypesRepository = new RolesRepository(context);
                var userType = Role.Create("student");
                var userTypeInserted = userTypesRepository.InsertAsync(userType).Result;
                // ACT
                var result = userTypesRepository.GetByIdAsync(userTypeInserted.Id);
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
                var userTypeRepository = new RolesRepository(context);
                var userType = Role.Create("student");
                context.Roles.Add(userType);
                context.SaveChanges();
                // ACT
                userType.Update("teacher");
                var result = userTypeRepository.UpdateAsync(userType);
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
                var userTypesRepository = new RolesRepository(context);
                var userType = Role.Create("student");
                context.Roles.Add(userType);
                context.SaveChanges();
                // ACT
                var result = userTypesRepository.DeleteAsync(userType.Id);
                // ASSERT
                result.Result.Should().Be(true);
            });

        }
    }
}
