using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.Repository;
using Data.Core.Domain;
using FluentAssertions;
using FluentAssertions.Common;
using Xunit;
namespace TestManagementIntegrationTests.Base
{
    public class TestsRepositoryTests: BaseIntegrationTest
    {
        [Fact]
        public void Given_Tests_When_GetTestsAsyncIsCalled_Then_ShouldReturnZeroTests()
        {
            RunOnDatabase(context => {
                // ARRANGE 
                var testRepository = new TestsRepository(context);

                // ACT
                var tests = testRepository.GetTestsAsync();
                var counter = tests.Result.Count;
                // ASSERT
                counter.Should().Be(0);
            });
        }

        [Fact]
        public void Given_Test_When_NewTestIsAdded_Then_ShouldHaveOneTestInDatabase()
        {
            RunOnDatabase(context => {
                // ARRANGE 
                context.UserTypes.Add(UserType.Create("student"));
                context.SaveChanges();
                var userType = context.UserTypes.ToList().FirstOrDefault();

                context.Users.Add(User.Create(
                    "John", 
                    "Mark", 
                    "john.mark@gmail.com", 
                    "password", 
                    userType.Id
                    )
                );
                context.SaveChanges();

                var user = context.Users.ToList().FirstOrDefault();

                context.TestTypes.Add(TestType.Create("grila"));
                context.SaveChanges();

                var testType = context.TestTypes.ToList().FirstOrDefault();

                var testsRepository = new TestsRepository(context);
                var test = Test.Create(
                    "mytest",
                    "descriere",
                    user.Id,
                    testType.Id
                );
                var testInserted = testsRepository.InsertTestAsync(test).Result;
                // ACT
                var result = testsRepository.GetTestByIdAsync(testInserted.Id);
                // ASSERT
                result.Should().NotBe(null);
            });

        }
        [Fact]
        public void Given_Test_When_UpdateTestAsync_Then_ShouldBeTrue()
        {
            RunOnDatabase(context => {
                // ARRANGE 

                context.UserTypes.Add(UserType.Create("student"));
                context.SaveChanges();
                var userType = context.UserTypes.ToList().FirstOrDefault();

                context.Users.Add(User.Create(
                        "John",
                        "Mark",
                        "john.mark@gmail.com",
                        "password",
                        userType.Id
                    )
                );
                context.SaveChanges();

                var user = context.Users.ToList().FirstOrDefault();

                context.TestTypes.Add(TestType.Create("grila"));
                context.SaveChanges();

                var testType = context.TestTypes.ToList().FirstOrDefault();
                var testsRepository = new TestsRepository(context);

                var test = Test.Create(
                    "mytest",
                    "descriere",
                    user.Id,
                    testType.Id
                );

                context.Add(test);
                context.SaveChanges();
                test.Update("test2", "descrierile", user.Id, testType.Id);


                // ACT
                var result = testsRepository.UpdateTestAsync(test);
                // ASSERT
                result.Result.Should().Be(true);
            });

        }

        [Fact]
        public void Given_Test_When_DeleteTestAsync_Then_ShouldBeTrue()
        {
            RunOnDatabase(context => {
                // ARRANGE 
                context.UserTypes.Add(UserType.Create("student"));
                context.SaveChanges();
                var userType = context.UserTypes.ToList().FirstOrDefault();

                context.Users.Add(User.Create(
                        "John",
                        "Mark",
                        "john.mark@gmail.com",
                        "password",
                        userType.Id
                    )
                );
                context.SaveChanges();

                var user = context.Users.ToList().FirstOrDefault();

                context.TestTypes.Add(TestType.Create("grila"));
                context.SaveChanges();

                var testType = context.TestTypes.ToList().FirstOrDefault();
                var testsRepository = new TestsRepository(context);

                var test = Test.Create(
                    "mytest",
                    "descriere",
                    user.Id,
                    testType.Id
                );

                context.Add(test);
                context.SaveChanges();

                // ACT
                var result = testsRepository.DeleteTestAsync(test.Id);
                // ASSERT
                result.Result.Should().Be(true);
            });

        }
    }
}
