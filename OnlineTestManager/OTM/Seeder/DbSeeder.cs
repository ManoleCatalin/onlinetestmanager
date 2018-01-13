using System;
using System.Threading.Tasks;
using Constants;
using Data.Core.Domain;
using Data.Core.Interfaces;
using Data.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace OTM.Seeder
{
    public class DbSeeder
    {
        private readonly DatabaseContext _databaseContext;
        private readonly IServiceProvider _serviceProvider;
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly IGroupsRepository _groupsRepository;
        private readonly ITestTypesRepository _testTypesRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly ITestsRepository _testsRepository;
        private readonly IExercisesRepository _exercisesRepository;
        private readonly IAnswersRepository _answersRepository;


        public DbSeeder(
            DatabaseContext databaseContext,
            IServiceProvider serviceProvider,
            RoleManager<Role> roleManager,
            UserManager<User> userManager,
            IGroupsRepository groupsRepository,
            ITestTypesRepository testTypesRepository,
            IUsersRepository usersRepository,
            ITestsRepository testsRepository, 
            IExercisesRepository exercisesRepository,
            IAnswersRepository answersRepository)
        {
            _databaseContext = databaseContext;
            _serviceProvider = serviceProvider;
            _roleManager = roleManager;
            _userManager = userManager;
            _groupsRepository = groupsRepository;
            _testTypesRepository = testTypesRepository;
            _usersRepository = usersRepository;
            _testsRepository = testsRepository;
            _exercisesRepository = exercisesRepository;
            _answersRepository = answersRepository;
        }

        public async Task SeedAsync()
        {
            using (var serviceScope = _serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var service = serviceScope.ServiceProvider.GetService<DatabaseContext>();

                if (!await service.Roles.AnyAsync())
                {
                    await InsertRolesData();
                    await InsertUsersSampleData();
                }

                if (!await service.Groups.AnyAsync())
                {
                    await InsertGroupSampleData();
                }

                if (!await service.TestTypes.AnyAsync())
                {
                    await InsertTestTypesData();
                }

                if (!await service.Tests.AnyAsync())
                {
                    await InsertTestTemplateSampleData();
                }
            }
        }

        public async Task InsertRolesData()
        {
            var roleNames = RoleConstants.GetRoleNames();
            foreach (var roleName in roleNames)
            {
                if (!await _roleManager.RoleExistsAsync(roleName))
                {
                    await _roleManager.CreateAsync(Role.Create(roleName));
                }
            }

            await _databaseContext.SaveChangesAsync();
        }

        public async Task InsertTestTypesData()
        {
            var testTypeNames = TestTypesConstants.GetTestTypeNames();
            foreach (var testTypeName in testTypeNames)
            {
                var testType = TestType.Create(testTypeName);
                await _testTypesRepository.InsertAsync(testType);
            }

            await _databaseContext.SaveChangesAsync();
        }

        public async Task InsertUsersSampleData()
        {
            const int userCount = 5;

            var roleNames = RoleConstants.GetRoleNames();

            if (await _databaseContext.Users.AnyAsync())
                return;
            foreach (var roleName in roleNames)
            {
                for (var i = 0; i < userCount; i++)
                {
                    var username = string.Format(roleName + "{0}", i);
                    var user = User.Create(username, username, username, username + "@gmail.com", username);
                    var result = await _userManager.CreateAsync(user, user.PasswordHash);
                    if (!result.Succeeded) throw new Exception(string.Format("Failed creating user with username {0} ", username));
                    await _userManager.AddToRoleAsync(user, roleName);
                }
            }

            await _databaseContext.SaveChangesAsync();
        }

        public async Task InsertGroupSampleData()
        {
            const string groupFormat = "Group{0}";
            const string descriptionFormat = "Description{0}";

            var teachers = await _usersRepository.GetAllByRoleName(RoleConstants.TeacherRoleName);
            teachers.Sort((x, emp2) => x.FirstName.CompareTo(emp2.FirstName));
            var students = await _usersRepository.GetAllByRoleName(RoleConstants.StudentRoleName);
            students.Sort((x, emp2) => x.FirstName.CompareTo(emp2.FirstName));

            for (var i = 0; i < teachers.Count; i++)
            {
                for (var j = 0; j < i; j++)
                {
                    var groupName = string.Format(groupFormat, j);
                    var descriptionName = string.Format(descriptionFormat, j);

                    var group = await _groupsRepository.InsertAsync(Group.Create(groupName, descriptionName, teachers[i].Id));

                    for (var k = 0; k < i; k++)
                    {
                        await _groupsRepository.InsertStudentAsync(group.Id, students[k].Id);
                    }
                }
            }
        }

        public async Task InsertTestTemplateSampleData()
        {
            const int exerciseCount = 2;
            var teachers = await _usersRepository.GetAllByRoleName(RoleConstants.TeacherRoleName);
            teachers.Sort((x, emp2) => x.FirstName.CompareTo(emp2.FirstName));
            var testTypes = await _testTypesRepository.GetAllAsync();
            var multipleChoiceTestType = testTypes[0]; // TODO: obtain multiple choice test type in a safe and extensible way
            const string nameFormat = "Test{0}";
            const string descriptionFormat = "Description{0}";


            for (var i = 0; i < teachers.Count; i++)
            {

                for (var x = 0; x < i; x++)
                {
                    var name = string.Format(nameFormat, x);
                    var description = string.Format(descriptionFormat, x);


                    var testInstace = Test.Create(name, description, teachers[i].Id, multipleChoiceTestType.Id);
                    await _testsRepository.InsertAsync(testInstace);

                    const string exerciseDescriptionFormat = "{1} + {0} = ?";
                    for (var j = 0; j < exerciseCount; j++)
                    {

                        var exerciseDescription = string.Format(exerciseDescriptionFormat, j, x);
                        var exerciseEntity = Exercise.Create(exerciseDescription, testInstace.Id);

                        var insertedExercise = await _exercisesRepository.InsertAsync(exerciseEntity);

                        const string answerDescriptionFormat = "Answer is {0}";

                        const int answerCount = 2;
                        for (var k = 0; k < answerCount; k++)
                        {
                            const int integerPadding = 10;
                            var answerDescription = string.Format(answerDescriptionFormat, integerPadding + k + j + x);
                            var answerEntiry = Answer.Create(answerDescription, false, insertedExercise.Id);
                            await _answersRepository.InsertAsync(answerEntiry);
                        }
                    }
                }    
            }
        }
    }
}