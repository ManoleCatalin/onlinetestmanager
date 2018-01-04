using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using AutoMapper;
using Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Data.Core.Domain;
using Data.Core.Interfaces;
using Data.Persistence;
using Microsoft.AspNetCore.Authorization;
using OTM.UserContext;
using OTM.ViewModels.ScheduledTest;

namespace OTM.Controllers
{
    [Authorize(Roles = RoleConstants.TeacherRoleName)]
    [Route("[Controller]/[Action]")]
    public class ScheduledTestsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ITestInstancesRepository _testInstancesRepository;
        private readonly ITestsRepository _testsRepository;
        private readonly Guid _userId;
        private readonly IGroupsRepository _groupsRepository;
        private readonly IAnswersRepository _answersRepository;
        private readonly IExercisesRepository _exercisesRepository;
        private readonly ITestTypesRepository _testTypesRepository;

        public ScheduledTestsController (ITestInstancesRepository testInstancesRepository, IMapper mapper,IUserContext userContext,IGroupsRepository groupsRepository, ITestsRepository testsRepository, IAnswersRepository answersRepository, IExercisesRepository exercisesRepository, ITestTypesRepository testTypesRepository)
        {
            _testInstancesRepository = testInstancesRepository;
            _mapper = mapper;
            _groupsRepository = groupsRepository;
            _testsRepository = testsRepository;
            _answersRepository = answersRepository;
            _exercisesRepository = exercisesRepository;
            _testTypesRepository = testTypesRepository;

            var userId = userContext.GetLogedInUserId();
            if (userId == null)
            {
                throw new ApplicationException("userId is null");
            }
            _userId = (Guid)userId;
        }

        // GET: ScheduledTests
        public async Task<IActionResult> Index()
        {
            var scheduledTests = await _testInstancesRepository.GetAllTestInstancesOfTeacherAsync(_userId);
            var indexScheduledTestsViewModel = _mapper.Map<List<IndexScheduledTestViewModel>>(scheduledTests);

            foreach (var item in indexScheduledTestsViewModel)
            {
                item.GroupName = _groupsRepository.GetByIdAsync(item.GroupId).Result.Name;
            }
            return View(indexScheduledTestsViewModel);
        }

        private List<User> DetailsGetAllUsersOfGroupByGroupId(Guid id)
        {
            var group =  _groupsRepository.GetByIdAsync(id).Result;
            var userGroupsEnumerator = new List<UserGroup>(group.UserGroups);
            var userList = new List<User>();
            foreach (var item in userGroupsEnumerator)
            {
                userList.Add(item.User);
            }
            return userList;
        }
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var testInstance = await _testInstancesRepository.GetByIdAsync(id);
            var test = await _testsRepository.GetByIdAsync(testInstance.TestId);
            var testType = _testTypesRepository.GetByIdAsync(test.TestTypeId).Result.Type;

            var group = await _groupsRepository.GetByIdAsync(testInstance.GroupId);
            var userList = DetailsGetAllUsersOfGroupByGroupId(group.Id);

            var exercises = await _exercisesRepository.GetAllExercisesOfTestAsync(test.Id);
            var exerciseList = new List<ScheduledTestDetailsExercise>();
            foreach (var item in exercises)
            {
                exerciseList.Add(new ScheduledTestDetailsExercise()
                {
                    Description = item.Description,
                    Answers = await _answersRepository.GetAllAnswersOfExerciseAsync(item.Id)
                });
            }
            var detailsScheduledTestViewModel = new DetailsScheduledTestViewModel();
            var testDetails = new ScheduledTestDetailsTest();
            testDetails.TestType = testType;
            testDetails.Description = test.Description;
            testDetails.Name = test.Name;
            testDetails.Exercises = exerciseList;
            
            var groupDetails = new ScheduledTestDetailsGroup();
            groupDetails.Description = group.Description;
            groupDetails.Name = group.Name;
            groupDetails.Users = userList;
            detailsScheduledTestViewModel.TestDetails = testDetails;
            detailsScheduledTestViewModel.GroupDetails = groupDetails;
            detailsScheduledTestViewModel.Id = id;




            return View(detailsScheduledTestViewModel);
        }

        private List<SelectListItem> GetAllGroupsByTeacherId(Guid teacherId)
        {
            return _groupsRepository.GetAllGroupsOfTeacherAsync(teacherId)
                .Result.Select(element => new SelectListItem
                {
                    Value = element.Id.ToString(),
                    Text = element.Name
                })
                .ToList();
        }

        private List<SelectListItem> GetAllTestsByTeacherId(Guid teacherId)
        {
            return _testsRepository.GetAllTestsOfTeacherAsync(teacherId)
                .Result.Select(element => new SelectListItem
                {
                    Value = element.Id.ToString(),
                    Text = element.Name
                })
                .ToList();
        }

        // GET: ScheduledTests/Create
        [HttpGet]
        public IActionResult Create()
        {
            var groups = GetAllGroupsByTeacherId(_userId);
            var tests = GetAllTestsByTeacherId(_userId);

            var createScheduledTestViewModel = new CreateScheduledTestViewModel();
            createScheduledTestViewModel.Groups = groups;
            createScheduledTestViewModel.Tests = tests;

                
            return View(createScheduledTestViewModel);
        }

        // POST: ScheduledTests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateScheduledTestViewModel createScheduledTestViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(createScheduledTestViewModel);
            }
            var duration = createScheduledTestViewModel.Duration;
            var groupId = Guid.Parse(createScheduledTestViewModel.Group);
            var testId = Guid.Parse(createScheduledTestViewModel.Test);
            var startDate = createScheduledTestViewModel.StartDateTime;

            var scheduledTest =
                await _testInstancesRepository.InsertAsync(TestInstance.Create("", duration, groupId, testId, startDate));

            return RedirectToAction(nameof(Index));
        }

        //// GET: ScheduledTests/Edit/5
        public IActionResult Edit(Guid id)
        {
            var scheduledTest = _testInstancesRepository.GetByIdAsync(id).Result;
            var editScheduledTestViewModel = _mapper.Map<EditScheduledTestViewModel>(scheduledTest);

            var groups = GetAllGroupsByTeacherId(_userId);
            var tests = GetAllTestsByTeacherId(_userId);

            editScheduledTestViewModel.Groups = groups;
            editScheduledTestViewModel.Tests = tests;

            return View(editScheduledTestViewModel);
        }

        // POST: ScheduledTests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditScheduledTestViewModel editScheduledTestViewModel)
        {
          
            if (!ModelState.IsValid)
                return View(editScheduledTestViewModel);

            var scheduledTest = _testInstancesRepository.GetByIdAsync(editScheduledTestViewModel.Id).Result;

            var duration = editScheduledTestViewModel.Duration;
            var startTime = editScheduledTestViewModel.StartDateTime;
            var groupId = Guid.Parse(editScheduledTestViewModel.Group);
            var testId = Guid.Parse(editScheduledTestViewModel.Test);

            scheduledTest.Update("",duration,groupId,testId,startTime);
            await _testInstancesRepository.UpdateAsync(scheduledTest);

            return RedirectToAction(nameof(Index));
        }

        // GET: ScheduledTests/Delete/5
        public IActionResult Delete(Guid id)
        {

            var scheduledTest = _testInstancesRepository.GetByIdAsync(id).Result;

            var deleteScheduledTestViewModel = _mapper.Map<DeleteScheduleTestViewModel>(scheduledTest);

            var group = _groupsRepository.GetByIdAsync(scheduledTest.GroupId).Result.Name;
            var test = _testsRepository.GetByIdAsync(scheduledTest.TestId).Result.Name;

            deleteScheduledTestViewModel.Group = group;
            deleteScheduledTestViewModel.Test = test;

            return View(deleteScheduledTestViewModel);
        }

        // POST: ScheduledTests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(DeleteScheduleTestViewModel deleteScheduledTestViewModel)
        {
            await _testInstancesRepository.DeleteAsync(deleteScheduledTestViewModel.Id);
            return RedirectToAction(nameof(Index));
        }

    }
}
