using System.Linq;
using Business.Repository;
using Data.Core.Domain;
using FluentAssertions;
using Xunit;

namespace TestManagementIntegrationTests.Base
{
    public class GradesRepositoryTests: BaseIntegrationTest
    {
        [Fact]
        public void Given_Grades_When_GetGradesAsyncsIsCalled_Then_ShouldReturnZeroGrades()
        {
            RunOnDatabase(context => {
                // ARRANGE 
                var gradesRepository = new GradesRepository(context);

                // ACT
                var grades = gradesRepository.GetGradesAsync();
                var counter = grades.Result.Count;
                // ASSERT
                counter.Should().Be(0);
            });
        }

        [Fact]
        public void Given_Grades_When_NewGradeIsAdded_Then_ShouldHaveOneGradeInDatabase()
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

                context.TestTypes.Add(TestType.Create("Grila"));
                context.SaveChanges();

                var testType = context.TestTypes.ToList().FirstOrDefault();
                context.Tests.Add(Test.Create("Test", "Test surpriza", user.Id, testType.Id));
                context.SaveChanges();

                var test = context.Tests.ToList().FirstOrDefault();
                var group = context.Groups.ToList().FirstOrDefault();
                context.TestInstances.Add(TestInstance.Create("Tokenel", 50, group.Id, test.Id));
                context.SaveChanges();

                var testInstance = context.TestInstances.ToList().FirstOrDefault();
                var gradesRepository = new GradesRepository(context);
                var grade = Grade.Create(7, user.Id, testInstance.Id);


                gradesRepository.InsertGradeAsync(grade).ConfigureAwait(true);
                // ACT
                var result = gradesRepository.GetGradeAsync(user.Id, testInstance.Id);
                // ASSERT
                result.Should().NotBe(null);
            });

        }

        [Fact]
        public void Given_Grade_When_UpdateGradeAsync_Then_ShouldBeTrue()
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

                context.TestTypes.Add(TestType.Create("Grila"));
                context.SaveChanges();

                var testType = context.TestTypes.ToList().FirstOrDefault();
                context.Tests.Add(Test.Create("Test", "Test surpriza", user.Id, testType.Id));
                context.SaveChanges();

                var test = context.Tests.ToList().FirstOrDefault();
                var group = context.Groups.ToList().FirstOrDefault();
                context.TestInstances.Add(TestInstance.Create("Tokenel", 50, group.Id, test.Id));
                context.SaveChanges();

                var testInstance = context.TestInstances.ToList().FirstOrDefault();
                var gradesRepository = new GradesRepository(context);
                var grade = Grade.Create(7, user.Id, testInstance.Id);

                context.Add(grade);
                context.SaveChanges();

                grade.Update(10);


                // ACT
                var result = gradesRepository.UpdateGradeAsync(grade);
                // ASSERT
                result.Result.Should().Be(true);
            });

        }

        [Fact]
        public void Given_Grade_When_DeleteGradeAsync_Then_ShouldBeTrue()
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

                context.TestTypes.Add(TestType.Create("Grila"));
                context.SaveChanges();

                var testType = context.TestTypes.ToList().FirstOrDefault();
                context.Tests.Add(Test.Create("Test", "Test surpriza", user.Id, testType.Id));
                context.SaveChanges();

                var test = context.Tests.ToList().FirstOrDefault();
                var group = context.Groups.ToList().FirstOrDefault();
                context.TestInstances.Add(TestInstance.Create("Tokenel", 50, group.Id, test.Id));
                context.SaveChanges();

                var testInstance = context.TestInstances.ToList().FirstOrDefault();
                var gradesRepository = new GradesRepository(context);
                var grade = Grade.Create(7, user.Id, testInstance.Id);

                context.Add(grade);
                context.SaveChanges();



                // ACT
                var result = gradesRepository.DeleteGradeAsync(grade.UserId, grade.TestInstanceId);
                // ASSERT
                result.Result.Should().Be(true);
            });

        }
    }
}
