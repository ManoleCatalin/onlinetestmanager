using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data.Core.Domain;
using Data.Core.Interfaces;
using Data.Persistence;
using Microsoft.AspNetCore.Authorization;
using OTM.ViewModels.AnswerTemplate;

namespace OTM.Controllers
{
    [Authorize(Roles = RoleConstants.TeacherRoleName)]
    [Route("[Controller]/[Action]")]
    public class AnswerTemplatesController : Controller
    {
        private readonly DatabaseContext _context;
        private readonly IAnswersRepository _answersRepository;
        private readonly IMapper _mapper;

        public AnswerTemplatesController(DatabaseContext context, IAnswersRepository answersRepository, IMapper mapper)
        {
            _context = context;
            _answersRepository = answersRepository;
            _mapper = mapper;
        }

        public IActionResult Index(Guid testTemplateId, Guid execiseTemplateId)
        {
            var answers = _answersRepository.GetAllAnswersOfExerciseAsync(execiseTemplateId).Result;

            var indexAnswers = AutoMapper.Mapper.Map<List<IndexAnswer>>(answers);

            var indexAnswerTempaltesViewModel = new IndexAnswerTemplatesViewModel
            {
                TestTemplateId = testTemplateId,
                ExerciseTemplateId = execiseTemplateId,
                Answers = indexAnswers
            };

            return View(indexAnswerTempaltesViewModel);
        }


        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var answer = await _context.Answers
                .Include(a => a.Exercise)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (answer == null)
            {
                return NotFound();
            }

            return View(answer);
        }

        [HttpGet]
        public IActionResult Create(Guid testTemplateId, Guid exerciseTemplateId)
        {

            var createExerciseTemplatesViewModel = new CreateAnswerTemplatesViewModel
            {
                TestTemplateId = testTemplateId,
                ExerciseTemplateId = exerciseTemplateId
            };

            return View(createExerciseTemplatesViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateAnswerTemplatesViewModel createAnswerTemplatesViewModel)
        {
            if (ModelState.IsValid)
            {

                var answer = Answer.Create(createAnswerTemplatesViewModel.Description,
                    createAnswerTemplatesViewModel.Correct, createAnswerTemplatesViewModel.ExerciseTemplateId);

                var insertedAnswer = await _answersRepository.InsertAsync(answer);

                return RedirectToAction(nameof(Create), 
                    new {testTemplateId = createAnswerTemplatesViewModel.TestTemplateId,
                         exerciseTemplateId = createAnswerTemplatesViewModel.ExerciseTemplateId});
            }

            return View(createAnswerTemplatesViewModel);
        }

        [HttpGet]
        public IActionResult Edit(Guid testTemplateId, Guid exerciseTemplateId, Guid answerTemplateId)
        {
            var answer = _answersRepository.GetByIdAsync(answerTemplateId).Result;

            var editAnswerTemplatesViewModel = _mapper.Map<EditAnswerTemplatesViewModel>(answer);

            editAnswerTemplatesViewModel.AnswerTemplateId = answerTemplateId;
            editAnswerTemplatesViewModel.ExerciseTemplateId = exerciseTemplateId;
            editAnswerTemplatesViewModel.TestTemplateId = testTemplateId;

            return View(editAnswerTemplatesViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditAnswerTemplatesViewModel editAnswerTemplatesViewModel)
        {
            if (ModelState.IsValid)
            {
                var answer = await _answersRepository.GetByIdAsync(editAnswerTemplatesViewModel.AnswerTemplateId);
                answer.Update(editAnswerTemplatesViewModel.Description, editAnswerTemplatesViewModel.Correct, editAnswerTemplatesViewModel.ExerciseTemplateId);
                var updatedAnswer = _answersRepository.UpdateAsync(answer);

                return RedirectToAction(nameof(Edit), "ExerciseTemplates", new
                {
                    testTemplateId = editAnswerTemplatesViewModel.TestTemplateId,
                    exerciseTemplateId = editAnswerTemplatesViewModel.ExerciseTemplateId
                });
            }

            return View(editAnswerTemplatesViewModel);
        }

        public IActionResult Delete(Guid answerTemplateId,Guid testTemplateId, Guid exerciseTemplateId)
        {
            var answer = _answersRepository.GetByIdAsync(answerTemplateId).Result;

            var deleteAnswerTemplatesViewModel = _mapper.Map<DeleteAnswerTemplatesViewModel>(answer);
            deleteAnswerTemplatesViewModel.TestTemplateId = testTemplateId;
            deleteAnswerTemplatesViewModel.ExerciseTemplateId = exerciseTemplateId;

            return View(deleteAnswerTemplatesViewModel);
        }

        // POST: AnswerTemplates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(DeleteAnswerTemplatesViewModel deleteAnswerTemplatesViewModel)
        {
            await _answersRepository.DeleteAsync(deleteAnswerTemplatesViewModel.Id);
            return RedirectToAction(nameof(Edit),"ExerciseTemplates",new {TestTemplateId = deleteAnswerTemplatesViewModel.TestTemplateId, ExerciseTemplateId = deleteAnswerTemplatesViewModel.ExerciseTemplateId } );
        }

        private bool AnswerExists(Guid id)
        {
            return _context.Answers.Any(e => e.Id == id);
        }
    }
}
