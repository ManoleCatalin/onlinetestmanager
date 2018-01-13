using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Constants;
using Microsoft.AspNetCore.Mvc;
using Data.Core.Domain;
using Data.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using OTM.ViewModels.ExerciseTemplate;

namespace OTM.Controllers
{
    [Authorize(Roles = RoleConstants.TeacherRoleName)]
    [Route("[Controller]/[Action]")]
    public class ExerciseTemplatesController : Controller
    {
        private readonly IExercisesRepository _exercisesRepository;
        private readonly IAnswersRepository _answersRepository;
        private readonly IMapper _mapper;

        public ExerciseTemplatesController(IExercisesRepository exercisesRepository,
            IMapper mapper, IAnswersRepository answersRepository)
        {
            _exercisesRepository = exercisesRepository;
            _mapper = mapper;
            _answersRepository = answersRepository;
        }

        [HttpGet]
        public IActionResult Index(Guid testTemplateId)
        {
            var exercises = _exercisesRepository.GetAllExercisesOfTestAsync(testTemplateId).Result;

            var indexExercises = _mapper.Map<List<IndexExercise>>(exercises);

            var exerciseTemplatesViewModels = new IndexExerciseTemplatesViewModel
            {
                TestTemplateId = testTemplateId,
                IndexExercises = indexExercises
            };

            return View(exerciseTemplatesViewModels);
        }

        //// GET: ExerciseTemplates/Details/5
        //public async Task<IActionResult> Details(Guid exerciseTemplateId, Guid testTemplateId)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var exercise = await _context.Exercises
        //        .Include(e => e.Test)
        //        .SingleOrDefaultAsync(m => m.Id == id);
        //    if (exercise == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(exercise);
        //}

        // GET: ExerciseTemplates/Create
        public IActionResult Create(Guid testTemplateId)
        {
            var createExeciseTemplatesViewModel = new CreateExerciseTemplatesViewModel {TestTemplateId = testTemplateId};

            return View(createExeciseTemplatesViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateExerciseTemplatesViewModel createExerciseTemplatesViewModel)
        {
            if (ModelState.IsValid)
            {
                var exercise = Exercise.Create(createExerciseTemplatesViewModel.Description,
                    createExerciseTemplatesViewModel.TestTemplateId);

                var insertedExercise = await _exercisesRepository.InsertAsync(exercise);

                return RedirectToAction(nameof(Create), "AnswerTemplates",
                    new {testTemplateId = createExerciseTemplatesViewModel.TestTemplateId, exerciseTemplateId = insertedExercise.Id});
            }
            
            return View(createExerciseTemplatesViewModel);
        }
        
        public async Task<IActionResult> Edit(Guid exerciseTemplateId, Guid testTemplateId)
        {
            var editExerciseTemplatesViewModel = new EditExerciseTemplatesViewModel
            {
                Id = exerciseTemplateId,
                TestTemplateId = testTemplateId
            };

            var exercise = await _exercisesRepository.GetByIdAsync(exerciseTemplateId);
            var answers = await  _answersRepository.GetAllAnswersOfExerciseAsync(exerciseTemplateId);

            var editAnswer = _mapper.Map<List<EditAnswer>>(answers);

            editExerciseTemplatesViewModel.Answers = editAnswer;
            editExerciseTemplatesViewModel.Description = exercise.Description;

            return View(editExerciseTemplatesViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditExerciseTemplatesViewModel editExerciseTemplatesViewModel)
        {
            if (ModelState.IsValid)
            {
                var exercise = await _exercisesRepository.GetByIdAsync(editExerciseTemplatesViewModel.Id);

                exercise.Update(editExerciseTemplatesViewModel.Description,
                    editExerciseTemplatesViewModel.TestTemplateId,false);
            
                await _exercisesRepository.UpdateAsync(exercise);

                return RedirectToAction(nameof(Edit), "TestTemplates",
                    new { id = editExerciseTemplatesViewModel.TestTemplateId});
            }
            
            return View(editExerciseTemplatesViewModel);
        }

        public IActionResult Delete(Guid exerciseTemplateId, Guid testTemplateId)
        {

            var exercise = _exercisesRepository.GetByIdAsync(exerciseTemplateId).Result;

            var deleteExerciseTemplatesViewModel = _mapper.Map<DeleteExerciseTemplateViewModel>(exercise);
            deleteExerciseTemplatesViewModel.TestTemplateId = testTemplateId;

            return View(deleteExerciseTemplatesViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(DeleteExerciseTemplateViewModel deleteExerciseTemplatesViewModel)
        {
            await _exercisesRepository.DeleteAsync(deleteExerciseTemplatesViewModel.Id);
            return RedirectToAction(nameof(Edit),"TestTemplates", new {Id = deleteExerciseTemplatesViewModel.TestTemplateId});
        }
    }
}

