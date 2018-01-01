using System;
using System.Collections.Generic;
using System.Linq;
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
            var createExeciseTemplatesViewModel = new CreateExeciseTemplatesViewModel {TestTemplateId = testTemplateId};

            return View(createExeciseTemplatesViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateExeciseTemplatesViewModel createExeciseTemplatesViewModel)
        {
            if (ModelState.IsValid)
            {
                var exercise = Exercise.Create(createExeciseTemplatesViewModel.Description,
                    createExeciseTemplatesViewModel.TestTemplateId);

                var insertedExercise = await _exercisesRepository.InsertAsync(exercise);

                return RedirectToAction(nameof(Create), "AnswerTemplates",
                    new {testTemplateId = createExeciseTemplatesViewModel.TestTemplateId, exerciseTemplateId = insertedExercise.Id});
            }
            
            return View(createExeciseTemplatesViewModel);
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
                    editExerciseTemplatesViewModel.TestTemplateId);
            
                await _exercisesRepository.UpdateAsync(exercise);

                return RedirectToAction(nameof(Edit), "TestTemplates",
                    new { id = editExerciseTemplatesViewModel.TestTemplateId});
            }
            
            return View(editExerciseTemplatesViewModel);
        }

        // GET: ExerciseTemplates/Delete/5
        //public async Task<IActionResult> Delete(Guid? id)
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

        //// POST: ExerciseTemplates/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(Guid id)
        //{
        //    var exercise = await _context.Exercises.SingleOrDefaultAsync(m => m.Id == id);
        //    _context.Exercises.Remove(exercise);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}
    }
}

