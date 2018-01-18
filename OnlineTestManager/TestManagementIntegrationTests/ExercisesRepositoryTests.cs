using System.Linq;
using Business.Repository;
using Data.Core.Domain;
using Data.Persistence;
using FluentAssertions;
using TestManagementIntegrationTests.Base;
using Xunit;

namespace TestManagementIntegrationTests
{
    public class ExercisesRepositoryTests : BaseIntegrationTest
    {
        [Fact]
        public void Given_Exercise_When_GetExercisesAsyncsIsCalled_Then_ShouldReturnZeroExercises()
        {
            RunOnDatabase(context =>
            {
                // ARRANGE 
                var exercisesRepository = new ExercisesRepository(context);

                // ACT
                var exercise = exercisesRepository.GetAllAsync();
                var counter = exercise.Result.Count;

                // ASSERT
                counter.Should().Be(0);
            });
        }

        [Fact]
        public void Given_Exercise_When_NewExerciseIsAdded_Then_ShouldHaveOneExerciseInDatabase()
        {
            RunOnDatabase(context => {
                // ARRANGE 
                DatabaseContext databaseContext = context;

                var exercisesRepository = new ExercisesRepository(context);

                databaseContext.Roles.Add(Role.Create("student"));
                databaseContext.SaveChanges();
                var userType = databaseContext.Roles.FirstOrDefault();

                databaseContext.TestTypes.Add(TestType.Create("Grila"));
                databaseContext.SaveChanges();
                var testType = databaseContext.TestTypes.FirstOrDefault();

                if (userType != null)
                    databaseContext.Users.Add(User.Create("Johny", "Bravo", "johnnybravo", "johnnybravo@gmail.com", "#$$RR#$TED"));
                databaseContext.SaveChanges();
                var user = databaseContext.Users.FirstOrDefault();

                if (user != null && testType != null)
                        databaseContext.Tests.Add(Test.Create("NumeleTestului", "DescriereaTextului", user.Id,
                            testType.Id));
                databaseContext.SaveChanges();
                var test = databaseContext.Tests.FirstOrDefault();


                if (test != null)
                {
                    var exercise = Exercise.Create("Problema1",test.Id);

                    var exerciseInserted = exercisesRepository.InsertAsync(exercise).Result;
                    // ACT
                    var result = exercisesRepository.GetByIdAsync(exerciseInserted.Id);
                    // ASSERT
                    result.Should().NotBe(null);
                }
            });
        }

        [Fact]
        public void Given_Exercise_When_UpdateExerciseAsync_Then_ShouldBeTrue()
        {
            RunOnDatabase(context => {
                // ARRANGE 
                DatabaseContext databaseContext = context;

                var exercisesRepository = new ExercisesRepository(context);

                databaseContext.Roles.Add(Role.Create("student"));
                databaseContext.SaveChanges();
                var userType = databaseContext.Roles.FirstOrDefault();

                databaseContext.TestTypes.Add(TestType.Create("Grila"));
                databaseContext.SaveChanges();
                var testType = databaseContext.TestTypes.FirstOrDefault();

                if (userType != null)
                    databaseContext.Users.Add(User.Create("Johny", "Bravo", "johnnybravo", "johnnybravo@gmail.com", "#$$RR#$TED"));
                databaseContext.SaveChanges();
                var user = databaseContext.Users.FirstOrDefault();

                if (user != null && testType != null)
                        databaseContext.Tests.Add(Test.Create("NumeleTestului", "DescriereaTextului", user.Id,
                            testType.Id));
                databaseContext.SaveChanges();
                var test = databaseContext.Tests.FirstOrDefault();


                if (test != null)
                {
                    var exercise = Exercise.Create("Problema1", test.Id);
                    databaseContext.Add(exercise);
                    databaseContext.SaveChanges();
                    exercise.Update("Problema2",test.Id, false);
                
                    // ACT
                    var result = exercisesRepository.UpdateAsync(exercise);
                    // ASSERT
                    result.Result.Should().Be(true);
                }
            });
        }

        [Fact]
        public void Given_Exercise_When_DeleteExerciseAsync_Then_ShouldBeTrue()
        {
            RunOnDatabase(context => {
                // ARRANGE 
                DatabaseContext databaseContext = context;

                var exercisesRepository = new ExercisesRepository(context);

                databaseContext.Roles.Add(Role.Create("student"));
                databaseContext.SaveChanges();
                var userType = databaseContext.Roles.FirstOrDefault();

                databaseContext.TestTypes.Add(TestType.Create("Grila"));
                databaseContext.SaveChanges();
                var testType = databaseContext.TestTypes.FirstOrDefault();

                if (userType != null)
                    databaseContext.Users.Add(User.Create("Johny", "Bravo", "johnnybravo", "johnnybravo@gmail.com", "#$$RR#$TED"));
                databaseContext.SaveChanges();
                var user = databaseContext.Users.FirstOrDefault();
                if (user != null && testType != null)
                        databaseContext.Tests.Add(Test.Create("NumeleTestului", "DescriereaTextului", user.Id,
                            testType.Id));
                databaseContext.SaveChanges();
                var test = databaseContext.Tests.FirstOrDefault();

                if (test != null)
                {
                    var exercise = Exercise.Create("Problema1", test.Id);
                    databaseContext.Add(exercise);
                    databaseContext.SaveChanges();

                    // ACT
                    var result = exercisesRepository.DeleteAsync(exercise.Id);
                    // ASSERT
                    result.Result.Should().Be(true);
                }
            });
        }
    }
}
