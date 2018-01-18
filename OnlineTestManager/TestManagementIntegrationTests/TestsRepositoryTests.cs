using System.Linq;
using Business.Repository;
using Data.Core.Domain;
using FluentAssertions;
using TestManagementIntegrationTests.Base;
using Xunit;

namespace TestManagementIntegrationTests
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
                var tests = testRepository.GetAllAsync();
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
                context.Roles.Add(Role.Create("student"));
                context.SaveChanges();
                var userType = context.Roles.ToList().FirstOrDefault();

                if (userType != null)
                    context.Users.Add(User.Create(
                            "John",
                            "Mark",
                            "john.mar",
                            "john.mark@gmail.com",
                            "password"
                        )
                    );
                context.SaveChanges();

                var user = context.Users.ToList().FirstOrDefault();

                context.TestTypes.Add(TestType.Create("grila"));
                context.SaveChanges();

                var testType = context.TestTypes.ToList().FirstOrDefault();

                var testsRepository = new TestsRepository(context);
                if (user == null) return;
                if (testType == null) return;
                var test = Test.Create(
                    "mytest",
                    "descriere",
                    user.Id,
                    testType.Id
                );
                var testInserted = testsRepository.InsertAsync(test).Result;
                // ACT
                var result = testsRepository.GetByIdAsync(testInserted.Id);
                // ASSERT
                result.Should().NotBe(null);
            });

        }
        [Fact]
        public void Given_Test_When_UpdateTestAsync_Then_ShouldBeTrue()
        {
            RunOnDatabase(context => {
                // ARRANGE 

                context.Roles.Add(Role.Create("student"));
                context.SaveChanges();
                var userType = context.Roles.ToList().FirstOrDefault();

                if (userType != null)
                    context.Users.Add(User.Create(
                            "John",
                            "Mark",
                            "john.mar",
                            "john.mark@gmail.com",
                            "password"
                        )
                    );
                context.SaveChanges();

                var user = context.Users.ToList().FirstOrDefault();

                context.TestTypes.Add(TestType.Create("grila"));
                context.SaveChanges();

                var testType = context.TestTypes.ToList().FirstOrDefault();
                var testsRepository = new TestsRepository(context);

                if (user == null) return;
                if (testType == null) return;
                var test = Test.Create(
                    "mytest",
                    "descriere",
                    user.Id,
                    testType.Id
                );

                context.Add(test);
                context.SaveChanges();
                test.Update("test2", "descrierile", user.Id, testType.Id, false);


                // ACT
                var result = testsRepository.UpdateAsync(test);
                // ASSERT
                result.Result.Should().Be(true);
            });

        }

        [Fact]
        public void Given_Test_When_DeleteTestAsync_Then_ShouldBeTrue()
        {
            RunOnDatabase(context => {
                // ARRANGE 
                context.Roles.Add(Role.Create("student"));
                context.SaveChanges();
                var userType = context.Roles.ToList().FirstOrDefault();

                if (userType != null)
                    context.Users.Add(User.Create(
                            "John",
                            "Mark",
                            "john.mark",
                            "john.mark@gmail.com",
                            "password"
                        )
                    );
                context.SaveChanges();

                var user = context.Users.ToList().FirstOrDefault();

                context.TestTypes.Add(TestType.Create("grila"));
                context.SaveChanges();

                var testType = context.TestTypes.ToList().FirstOrDefault();
                var testsRepository = new TestsRepository(context);

                if (testType == null) return;
                if (user == null) return;
                var test = Test.Create(
                    "mytest",
                    "descriere",
                    user.Id,
                    testType.Id
                );

                context.Add(test);
                context.SaveChanges();

                // ACT
                var result = testsRepository.DeleteAsync(test.Id);
                // ASSERT
                result.Result.Should().Be(true);
            });

        }
    }
}
