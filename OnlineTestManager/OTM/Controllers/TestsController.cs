using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Constants;
using Data.Core.Domain;
using Microsoft.AspNetCore.Mvc;
using Data.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using OTM.UserContext;
using OTM.ViewModels.Tests;

namespace OTM.Controllers
{
    [Authorize(Roles = RoleConstants.StudentRoleName)]
    [Route("[Controller]/[Action]")]
    public class TestsController : Controller
    {
        private readonly ITestInstancesRepository _testInstancesRepository;
        private readonly Guid _userId;
        private readonly ITestsRepository _testsRepository;
        private readonly IExercisesRepository _exercisesRepository;

        public TestsController(ITestInstancesRepository testInstancesRepository, 
            IUserContext userContext, 
            ITestsRepository testsRepository, 
            IExercisesRepository exercisesRepository)
        {
            _testInstancesRepository = testInstancesRepository;
            _testsRepository = testsRepository;
            _exercisesRepository = exercisesRepository;

            var userId = userContext.GetLogedInUserId();
            if (userId == null)
            {
                throw new ApplicationException("userId is null");
            }
            _userId = (Guid)userId;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var tests = await _testInstancesRepository.GetAllTestInstancesOfStudentAsync(_userId);
            if (tests == null)
            {
                return NotFound();
            }
            var listIndexTestsViewModel = new List<IndexTestsViewModel>();
            foreach (var item in tests)
            {
                var test = await _testsRepository.GetByIdAsync(item.TestId);
                listIndexTestsViewModel.Add(new IndexTestsViewModel()
                {
                    Id = item.Id,
                    Description = test.Description,
                    Duration = item.Duration,
                    Name = test.Name,
                    Ongoing = (DateTime.Now > item.StartedAt && DateTime.Now < item.StartedAt.AddMinutes(item.Duration)),
                    StartDate = item.StartedAt

                });
            }

            return View(listIndexTestsViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Display(Guid id)
        {
            var exercise = await _testInstancesRepository.GetNextExerciseAsync(_userId,id);
            if (exercise == null)
            {
                return RedirectToAction(nameof(Finished),  new { id = id });
            }
            var displayTestsViewModel = new DisplayTestsViewModel();
            displayTestsViewModel.TestInstanceId = id;
            displayTestsViewModel.Description = exercise.Description;
            displayTestsViewModel.ExerciseId = exercise.Id;
            displayTestsViewModel.UserId = _userId;
            var answers = new List<MarkedCorrectAnswerDisplayTestsViewModel>();
            foreach(var item in exercise.Answers)
            {
                answers.Add(new MarkedCorrectAnswerDisplayTestsViewModel()
                {
                    Id = item.Id,
                    Description = item.Description
                });
            }
            displayTestsViewModel.Answers = answers;
            return View(displayTestsViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Display(DisplayTestsViewModel displayTestViewModel)
        {
            if (ModelState.IsValid)
            {
                var exerciseResponse = new ExerciseResponse();
                exerciseResponse.UserId = displayTestViewModel.UserId;
                exerciseResponse.ExerciseId = displayTestViewModel.ExerciseId;
                exerciseResponse.TestInstanceId = displayTestViewModel.TestInstanceId;
                var answers = new List<MarkedAsCorrect>();
                foreach (var item in displayTestViewModel.Answers)
                {
                    if (item.Correct == true)
                    {
                        answers.Add(new MarkedAsCorrect()
                        {
                            AnswerId = item.Id,
                            ExerciseId = displayTestViewModel.ExerciseId,
                            TestInstanceId = displayTestViewModel.TestInstanceId,
                            UserId = displayTestViewModel.UserId
                        });
                    }   
                }
                exerciseResponse.MarkedAsCorrects = answers;
                var exercise = await _testInstancesRepository.InsertExerciseResponseAsync(exerciseResponse);
                return RedirectToAction(nameof(Display), new { id = displayTestViewModel.TestInstanceId });
            }

            return View(displayTestViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Finished(Guid id)
        {
            var testInstance = await _testInstancesRepository.GetByIdAsync(id);
            if (testInstance == null)
            {
                return NotFound();
            }
            var test = await _testsRepository.GetByIdAsync(testInstance.TestId);
            if (test == null)
            {
                return NotFound();
            }
            var finishedTestsViewModel = new FinishedTestsViewModel();
            finishedTestsViewModel.Description = test.Description;
            finishedTestsViewModel.Name = test.Name;
            return View(finishedTestsViewModel);
        }

    }
}
