using System.Diagnostics;
using System.Linq;
using Data.Core.Domain;
using Data.Persistence;
using FluentAssertions;
using TestManagementIntegrationTests.Base;
using Xunit;

namespace TestManagementIntegrationTests
{
    public class TestDb : BaseIntegrationTest
    {
        [Fact]
        public void Test1()
        {
            RunOnDatabase(ctx =>
            {
                //Arrange
                DatabaseContext databaseContext = ctx;

                databaseContext.UserTypes.Add(UserType.Create("student"));
                databaseContext.SaveChanges();

                var userType = databaseContext.UserTypes.ToList().FirstOrDefault();
                if (userType == null)
                {
                    false.Should().Be(true);
                }

                Debug.Assert(userType != null, nameof(userType) + " != null");
                databaseContext.Users.Add(User.Create("Johny", "Bravo", "johnnybravo@gmail.com", "#$$RR#$TED",
                    userType.Id));

                databaseContext.SaveChanges();

                //Act
                var users = databaseContext.Users.ToList();


                //Assert
                Assert.Single(users);

                var user = users.First();

                databaseContext.Groups.Add(Group.Create("A2", "Grupa A2", user.Id));
                databaseContext.SaveChanges();

                Assert.Single(databaseContext.Groups);
                var group = databaseContext.Groups.First();


                databaseContext.UserGroups.Add(UserGroup.Create(user.Id, group.Id));
                databaseContext.SaveChanges();

                Assert.Single(databaseContext.UserGroups);
            });
        }

        [Fact]
        public void Test2()
        {
            RunOnDatabase(ctx =>
            {
                //Arrange
                DatabaseContext databaseContext = ctx;

                databaseContext.UserTypes.Add(UserType.Create("student"));
                databaseContext.SaveChanges();

                var userType = databaseContext.UserTypes.ToList().FirstOrDefault();
                if (userType == null)
                {
                    false.Should().Be(true);
                }

                Debug.Assert(userType != null, nameof(userType) + " != null");
                databaseContext.Users.Add(User.Create("Johny", "Bravo", "johnnybravo@gmail.com", "#$$RR#$TED",
                    userType.Id));

                databaseContext.SaveChanges();

                //Act
                var users = databaseContext.Users.ToList();


                //Assert
                Assert.Single(users);

                var user = users.First();

                databaseContext.TestTypes.Add(TestType.Create("Grila"));
                databaseContext.SaveChanges();

                var testType = databaseContext.TestTypes.ToList().FirstOrDefault();

                databaseContext.Tests.Add(Test.Create("TestulSuprem", "E cel mai smecher test", user.Id, testType.Id));
                databaseContext.SaveChanges();

                Assert.Single(databaseContext.Tests);
            });
        }

        [Fact]
        public void Test3()
        {
            RunOnDatabase(ctx =>
            {
                //Arrange
                DatabaseContext databaseContext = ctx;

                databaseContext.UserTypes.Add(UserType.Create("student"));
                databaseContext.SaveChanges();

                var userType = databaseContext.UserTypes.ToList().FirstOrDefault();
                if (userType == null)
                {
                    false.Should().Be(true);
                }

                Debug.Assert(userType != null, nameof(userType) + " != null");
                databaseContext.Users.Add(User.Create("Johny", "Bravo", "johnnybravo@gmail.com", "#$$RR#$TED",
                    userType.Id));

                databaseContext.SaveChanges();

                //Act
                var users = databaseContext.Users.ToList();


                //Assert
                Assert.Single(users);

                var user = users.First();

                databaseContext.Groups.Add(Group.Create("A2", "Grupa A2", user.Id));
                databaseContext.SaveChanges();

                Assert.Single(databaseContext.Groups);
                var group = databaseContext.Groups.First();


                databaseContext.UserGroups.Add(UserGroup.Create(user.Id, group.Id));
                databaseContext.SaveChanges();

                 Assert.Single(databaseContext.UserGroups);

                databaseContext.TestTypes.Add(TestType.Create("Grila"));
                databaseContext.SaveChanges();

                var testType = databaseContext.TestTypes.ToList().FirstOrDefault();

                databaseContext.Tests.Add(Test.Create("TestulSuprem", "E cel mai smecher test", user.Id, testType.Id));
                databaseContext.SaveChanges();

                var test = databaseContext.Tests.ToList().FirstOrDefault();

                Assert.Single(databaseContext.Tests);

                databaseContext.TestInstances.Add(TestInstance.Create("abcdece123", 3000, group.Id, test.Id));
                databaseContext.SaveChanges();

                var testInstance = databaseContext.TestInstances.ToList().FirstOrDefault();

                Assert.Single(databaseContext.TestInstances);

                databaseContext.Files.Add(File.Create("C:\\", "www.teste.ro", testInstance.Id));
                databaseContext.SaveChanges();

                Assert.Single(databaseContext.Files);

                databaseContext.Grades.Add(Grade.Create(8,user.Id,testInstance.Id));
                databaseContext.SaveChanges();

                Assert.Single(databaseContext.Grades);
            });
        }

        [Fact]
        public void Test4()
        {
            RunOnDatabase(ctx =>
            {
                //Arrange
                DatabaseContext databaseContext = ctx;

                databaseContext.UserTypes.Add(UserType.Create("student"));
                databaseContext.SaveChanges();

                var userType = databaseContext.UserTypes.ToList().FirstOrDefault();
                if (userType == null)
                {
                    false.Should().Be(true);
                }

                Debug.Assert(userType != null, nameof(userType) + " != null");
                databaseContext.Users.Add(User.Create("Johny", "Bravo", "johnnybravo@gmail.com", "#$$RR#$TED",
                    userType.Id));

                databaseContext.SaveChanges();

                //Act
                var users = databaseContext.Users.ToList();


                //Assert
                Assert.Single(users);

                var user = users.First();

                databaseContext.TestTypes.Add(TestType.Create("Grila"));
                databaseContext.SaveChanges();

                var testType = databaseContext.TestTypes.ToList().FirstOrDefault();

                databaseContext.Tests.Add(Test.Create("TestulSuprem", "E cel mai smecher test", user.Id, testType.Id));
                databaseContext.SaveChanges();

                Assert.Single(databaseContext.Tests);

                var test = databaseContext.Tests.ToList().FirstOrDefault();
                databaseContext.Exercises.Add(Exercise.Create("Cat face 2 + 2?", test.Id));
                databaseContext.SaveChanges();

                var exercise = databaseContext.Exercises.ToList().FirstOrDefault();
                databaseContext.Answers.Add(Answer.Create("4", true, exercise.Id));
                databaseContext.SaveChanges();

                Assert.Single(databaseContext.Answers);


            });
        }
    }
}
