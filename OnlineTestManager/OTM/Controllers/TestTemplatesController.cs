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
using OTM.Models.TestTemplatesViewModels;
using OTM.UserContext;


namespace OTM.Controllers
{
    [Authorize(Roles = RoleConstants.TeacherRoleName)]
    [Route("[Controller]/[Action]")]
    public class TestTemplatesController : Controller
    {
        private readonly ITestsRepository _testsRepository;
        private readonly ITestTypesRepository _testTypesRepository;
        private readonly IMapper _mapper;
        private readonly Guid _userId;

        public TestTemplatesController(ITestsRepository testsRepository, IMapper mapper, IUserContext userContext, ITestTypesRepository testTypesRepository)
        {
            _testsRepository = testsRepository;
            _mapper = mapper;
            _testTypesRepository = testTypesRepository;

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
            var indexTestTemplatesViewModel = _mapper.Map<List<IndexTestTemplatesViewModel>>(testTemplates);
            return View(indexTestTemplatesViewModel);
        }

        //// GET: TestTemplates/Details/5
        //public async Task<IActionResult> Details(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var test = await _context.Tests
        //        .Include(t => t.TestType)
        //        .Include(t => t.User)
        //        .SingleOrDefaultAsync(m => m.Id == id);
        //    if (test == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(test);
        //}

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
            await _testsRepository.InsertAsync(testTemplateToCreate);

            return RedirectToAction(nameof(Index));
        }

        //// GET: TestTemplates/Edit/5
        //public async Task<IActionResult> Edit(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var test = await _context.Tests.SingleOrDefaultAsync(m => m.Id == id);
        //    if (test == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["TestTypeId"] = new SelectList(_context.TestTypes, "Id", "Type", test.TestTypeId);
        //    ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", test.UserId);
        //    return View(test);
        //}

        //// POST: TestTemplates/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Description,CreatedAt,UserId,TestTypeId")] Test test)
        //{
        //    if (id != test.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(test);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!TestExists(test.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["TestTypeId"] = new SelectList(_context.TestTypes, "Id", "Type", test.TestTypeId);
        //    ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", test.UserId);
        //    return View(test);
        //}

        //// GET: TestTemplates/Delete/5
        //public async Task<IActionResult> Delete(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var test = await _context.Tests
        //        .Include(t => t.TestType)
        //        .Include(t => t.User)
        //        .SingleOrDefaultAsync(m => m.Id == id);
        //    if (test == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(test);
        //}

        //// POST: TestTemplates/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(Guid id)
        //{
        //    var test = await _context.Tests.SingleOrDefaultAsync(m => m.Id == id);
        //    _context.Tests.Remove(test);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool TestExists(Guid id)
        //{
        //    return _context.Tests.Any(e => e.Id == id);
        //}
    }
}
