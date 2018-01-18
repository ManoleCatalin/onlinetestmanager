using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Data.Core.Domain;
using Data.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using OTM.UserContext;
using OTM.ViewModels.TestTemplates;


namespace OTM.Controllers
{
    [Authorize(Roles = RoleConstants.TeacherRoleName)]
    [Route("[Controller]/[Action]")]
    public class TestTemplatesController : Controller
    {
        private readonly ITestsRepository _testsRepository;
        private readonly IExercisesRepository _exercisesRepository;
        private readonly ITestTypesRepository _testTypesRepository;
        private readonly IMapper _mapper;
        private readonly Guid _userId;

        public TestTemplatesController(ITestsRepository testsRepository, IMapper mapper, 
            IUserContext userContext, 
            ITestTypesRepository testTypesRepository, 
            IExercisesRepository exercisesRepository)
        {
            _testsRepository = testsRepository;
            _mapper = mapper;
            _testTypesRepository = testTypesRepository;
            _exercisesRepository = exercisesRepository;

            var userId = userContext.GetLogedInUserId();
            if (userId == null)
            {
                throw new ApplicationException("userId is null");
            }
            _userId = (Guid)userId;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var testTemplates = _testsRepository.GetAllTestsOfTeacherAsync(_userId).Result;
            if (testTemplates == null)
            {
                return NotFound();
            }
                var indexTestTemplatesViewModel = _mapper.Map<IEnumerable<IndexTestTemplatesViewModel>>(testTemplates);
            return View(indexTestTemplatesViewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var createTestTemplatesViewModel = new CreateTestTemplatesViewModel();
            var testTypes = new List<SelectListItem>();
            
            foreach (var testType in _testTypesRepository.GetAllAsync().Result)
            {
                testTypes.Add(new SelectListItem
                {
                    Value = testType.Id.ToString(),
                    Text = testType.Type
                });
            }
            
            createTestTemplatesViewModel.TestTypes = testTypes;
            return View(createTestTemplatesViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateTestTemplatesViewModel createTestTemplatesViewModel)
        {
            if (!ModelState.IsValid)
                return View(createTestTemplatesViewModel);
            var testTemplateToCreate = Test.Create(createTestTemplatesViewModel.Name,
                createTestTemplatesViewModel.Description
                , _userId
                , Guid.Parse(createTestTemplatesViewModel.TestTypeId));
            var insertedTest = await _testsRepository.InsertAsync(testTemplateToCreate);

            return RedirectToAction(nameof(Edit), new {Id = insertedTest.Id});
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var test = _testsRepository.GetByIdAsync(id).Result;
            if (test == null)
            {
                return NotFound();
            }
            var editTestTemplatesViewModel = _mapper.Map<EditTestTemplatesViewModel>(test);

            var exercises = _exercisesRepository.GetAllExercisesOfTestAsync(id).Result;
            if (exercises == null)
            {
                return NotFound();
            }
            var editExercises = _mapper.Map<List<EditExercise>>(exercises);
            editTestTemplatesViewModel.Exercises = editExercises;

            return View(editTestTemplatesViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditTestTemplatesViewModel editTestTemplatesViewModel)
        {
            if (!ModelState.IsValid)
                return View(editTestTemplatesViewModel);

            var test = await _testsRepository.GetByIdAsync(editTestTemplatesViewModel.Id);
            if (test == null)
            {
                return NotFound();
            }
            test.Update(editTestTemplatesViewModel.Name, editTestTemplatesViewModel.Description, test.UserId, test.TestTypeId, false);
            await _testsRepository.UpdateAsync(test);                

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            var test = _testsRepository.GetByIdAsync(id).Result;
            if (test == null)
            {
                return NotFound();
            }
            var deleteTestTemplateViewModel = _mapper.Map<DeleteTestTemplateViewModel>(test);

            return View(deleteTestTemplateViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(DeleteTestTemplateViewModel deteDeleteTestTemplateViewModel)
        {
            var deletedTest = await _testsRepository.DeleteAsync(deteDeleteTestTemplateViewModel.Id);
            if (deletedTest == false)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
