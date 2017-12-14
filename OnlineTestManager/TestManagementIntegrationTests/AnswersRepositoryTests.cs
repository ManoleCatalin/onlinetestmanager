using System.Linq;
using Business.Repository;
using Data.Core.Domain;
using Data.Persistence;
using FluentAssertions;
using TestManagementIntegrationTests.Base;
using Xunit;

namespace TestManagementIntegrationTests
{
    public class AnswersRepositoryTests : BaseIntegrationTest
    {
        [Fact]
        public void Given_Answers_When_GetAnswersAsyncsIsCalled_Then_ShouldReturnZeroAnswers()
        {
            RunOnDatabase(context => {
                // ARRANGE 
                var answersRepository = new AnswersRepository(context);

                // ACT
                var answers = answersRepository.GetAllAsync();
                var counter = answers.Result.Count;
                // ASSERT
                counter.Should().Be(0);
            });
        }

        [Fact]
        public void Given_Answer_When_NewAnswerIsAdded_Then_ShouldHaveOneAnswerInDatabase()
        {
            RunOnDatabase(context => {
                // ARRANGE 
                DatabaseContext databaseContext = context;

                var answersRepository = new AnswersRepository(context);

                databaseContext.UserTypes.Add(UserType.Create("student"));
                databaseContext.SaveChanges();
                var userType = databaseContext.UserTypes.ToList().FirstOrDefault();

                databaseContext.TestTypes.Add(TestType.Create("Grila"));
                databaseContext.SaveChanges();
                var testType = databaseContext.TestTypes.ToList().FirstOrDefault();

                if (userType != null)
                    databaseContext.Users.Add(User.Create("Johny", "Bravo", "johnnybravo@gmail.com", "#$$RR#$TED",
                        userType.Id));
                databaseContext.SaveChanges();
                var user = databaseContext.Users.ToList().FirstOrDefault();

                if (user != null)
                    if (testType != null)
                        databaseContext.Tests.Add(Test.Create("NumeleTestului", "DescriereaTextului", user.Id,
                            testType.Id));
                databaseContext.SaveChanges();
                var test = databaseContext.Tests.ToList().FirstOrDefault();

                if (test != null) databaseContext.Exercises.Add(Exercise.Create("Problema1", test.Id));
                databaseContext.SaveChanges();
                var exercise = databaseContext.Exercises.ToList().FirstOrDefault();


                if (exercise != null)
                {
                    var answer = Answer.Create("RaspunsProblema1", true, exercise.Id);

                    var answerInserted = answersRepository.InsertAsync(answer).Result;
                    // ACT
                    var result = answersRepository.GetByIdAsync(answerInserted.Id);
                    // ASSERT
                    result.Should().NotBe(null);
                }
            });

        }
        [Fact]
        public void Given_Answer_When_UpdateAnswerAsync_Then_ShouldBeTrue()
        {
            RunOnDatabase(context => {
                // ARRANGE 

                DatabaseContext databaseContext = context;
                var answersRepository = new AnswersRepository(context);

                databaseContext.UserTypes.Add(UserType.Create("student"));
                databaseContext.SaveChanges();
                var userType = databaseContext.UserTypes.ToList().FirstOrDefault();

                databaseContext.TestTypes.Add(TestType.Create("Grila"));
                databaseContext.SaveChanges();
                var testType = databaseContext.TestTypes.ToList().FirstOrDefault();

                if (userType != null)
                    databaseContext.Users.Add(User.Create("Johny", "Bravo", "johnnybravo@gmail.com", "#$$RR#$TED",
                        userType.Id));
                databaseContext.SaveChanges();
                var user = databaseContext.Users.ToList().FirstOrDefault();

                if (user != null)
                    if (testType != null)
                        databaseContext.Tests.Add(Test.Create("NumeleTestului", "DescriereaTextului", user.Id,
                            testType.Id));
                databaseContext.SaveChanges();
                var test = databaseContext.Tests.ToList().FirstOrDefault();

                if (test != null) databaseContext.Exercises.Add(Exercise.Create("Problema1", test.Id));
                databaseContext.SaveChanges();
                var exercise = databaseContext.Exercises.ToList().FirstOrDefault();

                if (exercise != null)
                {
                    var answer = Answer.Create("RaspunsProblema1", true, exercise.Id);
                    databaseContext.Add(answer);
                    databaseContext.SaveChanges();
                    answer.Update("Raspuns Problema 2", false, exercise.Id);


                    // ACT
                    var result = answersRepository.UpdateAsync(answer);
                    // ASSERT
                    result.Result.Should().Be(true);
                }
            });

        }

        [Fact]
        public void Given_User_When_DeleteUserAsync_Then_ShouldBeTrue()
        {
            RunOnDatabase(context => {
                // ARRANGE 

                DatabaseContext databaseContext = context;
                var answersRepository = new AnswersRepository(context);

                databaseContext.UserTypes.Add(UserType.Create("student"));
                databaseContext.SaveChanges();
                var userType = databaseContext.UserTypes.ToList().FirstOrDefault();

                databaseContext.TestTypes.Add(TestType.Create("Grila"));
                databaseContext.SaveChanges();
                var testType = databaseContext.TestTypes.ToList().FirstOrDefault();

                if (userType != null)
                    databaseContext.Users.Add(User.Create("Johny", "Bravo", "johnnybravo@gmail.com", "#$$RR#$TED",
                        userType.Id));
                databaseContext.SaveChanges();
                var user = databaseContext.Users.ToList().FirstOrDefault();

                if (user != null)
                    if (testType != null)
                        databaseContext.Tests.Add(Test.Create("NumeleTestului", "DescriereaTextului", user.Id,
                            testType.Id));
                databaseContext.SaveChanges();
                var test = databaseContext.Tests.ToList().FirstOrDefault();

                if (test != null) databaseContext.Exercises.Add(Exercise.Create("Problema1", test.Id));
                databaseContext.SaveChanges();
                var exercise = databaseContext.Exercises.ToList().FirstOrDefault();

                if (exercise != null)
                {
                    var answer = Answer.Create("RaspunsProblema1", true, exercise.Id);
                    databaseContext.Add(answer);
                    databaseContext.SaveChanges();

                    // ACT
                    var result = answersRepository.DeleteAsync(answer.Id);
                    // ASSERT
                    result.Result.Should().Be(true);
                }
            });

        }
    }
}
