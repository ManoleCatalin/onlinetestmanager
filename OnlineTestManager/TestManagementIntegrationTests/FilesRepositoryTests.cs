using System.Linq;
using Business.Repository;
using Data.Core.Domain;
using Data.Persistence;
using FluentAssertions;
using TestManagementIntegrationTests.Base;
using Xunit;

namespace TestManagementIntegrationTests
{
    public class FilesRepositoryTests : BaseIntegrationTest
    {


        public File CreateFile(DatabaseContext context, string path, string url)
        {
            context.UserTypes.Add(UserType.Create("teacher"));
            context.SaveChanges();
            var userType = context.UserTypes.ToList().FirstOrDefault();

            context.Users.Add(User.Create("Johnny", "Bravo", "johnnybravo@gmail.com", "2G3GSDGDFG", userType.Id));
            context.SaveChanges();
            var user = context.Users.ToList().FirstOrDefault();

            context.Groups.Add(Group.Create("some group", "description", user.Id));
            context.SaveChanges();
            var group = context.Groups.ToList().FirstOrDefault();

            context.TestTypes.Add(TestType.Create("grila"));
            context.SaveChanges();
            var testType = context.TestTypes.ToList().FirstOrDefault();

            context.Tests.Add(Test.Create("Partial Exam Python", "No description needed", user.Id, testType.Id));
            context.SaveChanges();
            var test = context.Tests.ToList().FirstOrDefault();

            context.TestInstances.Add(TestInstance.Create("4f4fwefsd", 300, group.Id, test.Id));
            context.SaveChanges();
            var testInstance = context.TestInstances.ToList().FirstOrDefault();

            return File.Create(path, url, testInstance.Id);
        }

        [Fact]
        public void Given_Files_When_GetFilesAsyncsIsCalled_Then_ShouldReturnZeroFiles()
        {
            RunOnDatabase(context =>
            {
                // ARRANGE 
                var filesRepository = new FilesRepository(context);

                // ACT
                var files = filesRepository.GetFilesAsync();
                var counter = files.Result.Count;
                // ASSERT
                counter.Should().Be(0);
            });
        }

        [Fact]
        public void Given_Files_When_NewFileIsAdded_Then_ShouldHaveOneFileInDatabase()
        {
            RunOnDatabase(context =>
            {
                // ARRANGE 
                var filesRepository = new FilesRepository(context);

                var file = CreateFile(context, "C:\\folder\\file1.txt", "/download/5jtj5gitj4jrfs");
                var fileInserted = filesRepository.InsertFileAsync(file).Result;
                // ACT
                var result = filesRepository.GetFileByIdAsync(fileInserted.Id);
                // ASSERT
                result.Should().NotBe(null);
            });
        }

        [Fact]
        public void Given_File_When_UpdateFileAsync_Then_ShouldBeTrue()
        {
            RunOnDatabase(context =>
            {
                // ARRANGE 
                var filesRepository = new FilesRepository(context);
                var file = CreateFile(context, "C:\\folder\\file1.txt", "/download/5jtj5gitj4jrfs");
                context.Files.Add(file);
                context.SaveChanges();
                // ACT
                file.Update(file.Path, "/download/xabcxsd", file.TestInstanceId);
                var result = filesRepository.UpdateFileAsync(file);
                // ASSERT

                result.Result.Should().Be(true);
            });

        }

        [Fact]
        public void Given_File_When_DeleteFileAsync_Then_ShouldBeTrue()
        {
            RunOnDatabase(context =>
            {
                // ARRANGE 
                var filesRepository = new FilesRepository(context);
                var file = CreateFile(context, "C:\\folder\\file1.txt", "/download/5jtj5gitj4jrfs");
                context.Files.Add(file);
                context.SaveChanges();
                // ACT
                var result = filesRepository.DeleteFileAsync(file.Id);
                // ASSERT
                result.Result.Should().Be(true);
            });

        }
    }
}
