﻿using Business.Repository;
using Data.Core.Domain;
using FluentAssertions;
using TestManagementIntegrationTests.Base;
using Xunit;

namespace TestManagementIntegrationTests
{
    public class TestTypesRepositoryTests : BaseIntegrationTest
    {
        [Fact]
        public void Given_TestTypes_When_GetTestTypesAsyncsIsCalled_Then_ShouldReturnZeroTestTypes()
        {
            RunOnDatabase(context =>
            {
                // ARRANGE 
                var testTypesRepository = new TestTypesRepository(context);

                // ACT
                var testTypes = testTypesRepository.GetAllAsync();
                var counter = testTypes.Result.Count;
                // ASSERT
                counter.Should().Be(0);
            });
        }

        [Fact]
        public void Given_TestType_When_NewTestTypeIsAdded_Then_ShouldHaveOneTestTypeInDatabase()
        {
            RunOnDatabase(context =>
            {
                // ARRANGE 
                var testTypeRepository = new TestTypesRepository(context);
                var testType = TestType.Create("grila");
                var testTypeInserted = testTypeRepository.InsertAsync(testType).Result;
                // ACT
                var result = testTypeRepository.GetByIdAsync(testTypeInserted.Id);
                // ASSERT
                result.Should().NotBe(null);
            });
        }

        [Fact]
        public void Given_TestType_When_UpdateTestTypeAsync_Then_ShouldBeTrue()
        {
            RunOnDatabase(context => {
                // ARRANGE 
                var testTypeRepository = new TestTypesRepository(context);
                var testType = TestType.Create("grila");
                context.TestTypes.Add(testType);
                context.SaveChanges();
                // ACT
                testType.Update("normal", false);
                var result = testTypeRepository.UpdateAsync(testType);
                // ASSERT
                
                result.Result.Should().Be(true);
            });

        }

        [Fact]
        public void Given_TestType_When_DeleteTestTypeAsync_Then_ShouldBeTrue()
        {
            RunOnDatabase(context => {
                // ARRANGE 
                var testTypeRepository = new TestTypesRepository(context);
                var testType = TestType.Create("grila");
                context.TestTypes.Add(testType);
                context.SaveChanges();
                // ACT
                var result = testTypeRepository.DeleteAsync(testType.Id);
                // ASSERT
                result.Result.Should().Be(true);
            });

        }
    }
}
