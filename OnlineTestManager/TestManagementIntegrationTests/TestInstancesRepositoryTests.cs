using System;
using System.Linq;
using Business.Repository;
using Data.Core.Domain;
using FluentAssertions;
using TestManagementIntegrationTests.Base;
using Xunit;

namespace TestManagementIntegrationTests
{
    public class TestInstancesRepositoryTests : BaseIntegrationTest
    {
        [Fact]
        public void Given_TestInstances_When_GetTestInstancesAsyncsIsCalled_Then_ShouldReturnZeroTestInstances()
        {
            RunOnDatabase(context => {
                // ARRANGE 
                var testInstancesRepository = new TestInstancesRepository(context);

                // ACT
                var testInstances = testInstancesRepository.GetAllAsync();
                var counter = testInstances.Result.Count;
                // ASSERT
                counter.Should().Be(0);
            });
        }

        [Fact]
        public void Given_TestInstance_When_NewTestInstanceIsAdded_Then_ShouldHaveOneTestInstanceInDatabase()
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

                if (userId != null)
                {
                    var group = Group.Create(

                        "A2",
                        "grupa din anul III",
                        userId.Id

                    );
                    context.Groups.Add(group);
                }
                context.SaveChanges();

                var groupId = context.Groups.FirstOrDefault();

                context.TestTypes.Add(TestType.Create("Test_1"));
                context.SaveChanges();
                var testType = context.TestTypes.FirstOrDefault();

                if (userId != null && testType != null)
               context.Tests.Add(Test.Create("Testul nr 1", "Matematica", userId.Id, testType.Id));

                context.SaveChanges();
                var testId = context.Tests.FirstOrDefault();

                var testInstancesRepository = new TestInstancesRepository(context);
                if (groupId != null && testId != null)
                {
                   
                        var testInstance = TestInstance.Create(
                            100,
                            groupId.Id,
                            testId.Id, 
                            DateTime.Now
                        );

                        var testInstanceInserted = testInstancesRepository.InsertAsync(testInstance).Result;
                        // ACT
                        var result = testInstancesRepository.GetByIdAsync(testInstanceInserted.Id);
                        // ASSERT
                        result.Should().NotBe(null);
                    
                }
            });

        }

        [Fact]
        public void Given_TestInstance_When_UpdateTestInstanceAsync_Then_ShouldBeTrue()
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

                if (userId != null)
                {
                    var group = Group.Create(

                        "A2",
                        "grupa din anul III",
                        userId.Id

                    );
                    context.Groups.Add(group);
                }
                context.SaveChanges();

                var groupId = context.Groups.FirstOrDefault();

                context.TestTypes.Add(TestType.Create("Test_1"));
                context.SaveChanges();
                var testType = context.TestTypes.FirstOrDefault();

                if (userId != null && testType != null)
                        context.Tests.Add(Test.Create("Testul nr 1", "Matematica", userId.Id, testType.Id));
                context.SaveChanges();
                var testId = context.Tests.FirstOrDefault();

                var testInstancesRepository = new TestInstancesRepository(context);
                if (groupId == null) return;
                if (testId == null) return;
                var testInstance = TestInstance.Create(
                    100,
                    groupId.Id,
                    testId.Id, 
                    DateTime.Now
                );

                context.Add(testInstance);
                context.SaveChanges();

                testInstance.Update(200, groupId.Id, testId.Id, DateTime.Now, false);


                // ACT
                var result = testInstancesRepository.UpdateAsync(testInstance);
                // ASSERT
                result.Result.Should().Be(true);
            });

        }

        [Fact]
        public void Given_TestInstance_When_DeleteTestInstanceAsync_Then_ShouldBeTrue()
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

                if (userId != null)
                {
                    var group = Group.Create(

                        "A2",
                        "grupa din anul III",
                        userId.Id

                    );
                    context.Groups.Add(group);
                }
                context.SaveChanges();

                var groupId = context.Groups.FirstOrDefault();

                context.TestTypes.Add(TestType.Create("Test_1"));
                context.SaveChanges();
                var testType = context.TestTypes.FirstOrDefault();

                if (userId != null && testType != null)
                        context.Tests.Add(Test.Create("Testul nr 1", "Matematica", userId.Id, testType.Id));
                context.SaveChanges();
                var testId = context.Tests.FirstOrDefault();

                var testInstancesRepository = new TestInstancesRepository(context);
                if (groupId != null && testId != null)
                {
                   var testInstance = TestInstance.Create(
                            100,
                            groupId.Id,
                            testId.Id, 
                            DateTime.Now
                        );

                        context.Add(testInstance);
                        context.SaveChanges();

                        // ACT
                        var result = testInstancesRepository.DeleteAsync(testInstance.Id);
                        // ASSERT
                        result.Result.Should().Be(true);
                    
                }
            });

        }

    }
}
