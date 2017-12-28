using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Constants;
using Microsoft.AspNetCore.Mvc;
using Data.Core.Domain;
using Data.Core.Interfaces;
using Data.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using OTM.Controllers.Base;
using OTM.Models.GroupViewModels;

namespace OTM.Controllers
{
    [Authorize(Roles = RoleConstants.TeacherRoleName)]
    [Route("[Controller]/[Action]")]
    public class GroupsController : BaseController
    {
        private readonly IGroupsRepository _context;
        private readonly Guid _userId;

        public GroupsController(IGroupsRepository context, IHttpContextAccessor httpContextAccessor, INotificationHandler<DomainNotification> notifications) : base(notifications)
        {
            _context = context;
            _userId = new Guid(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }

        // GET: Groups
        public IActionResult Index()
        {
            var groups = _context.GetAllAsync().Result;
            return View(groups);
        }

        // GET: Groups/Details/5
        public IActionResult Details(Guid id)
        {
            var group = _context.GetByIdAsync(id).Result;
            if (group == null)
            {
                return NotFound();
            }

            return View(group);
        }

        // GET: Groups/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Groups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateGroupViewModel createGroupViewModel)
        {
            if (!ModelState.IsValid) return View(createGroupViewModel);

            var createdGroup = Group.Create(createGroupViewModel.Name, createGroupViewModel.Description, _userId);
            await _context.InsertAsync(createdGroup);

            if (IsValidOperation())
                ViewBag.Sucesso = "Group Created!";
            return RedirectToAction(nameof(Index));
        }

        // GET: Groups/Edit/5
        public IActionResult Edit(Guid id)
        {

            var group = _context.GetByIdAsync(id).Result;
            if (group == null)
            {
                return NotFound();
            }

            return View(group);
        }

        // POST: Groups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, string name, string description)
        {
            
            var updatedGroup = _context.GetByIdAsync(id).Result;
            updatedGroup.Update(name,description,_userId) ;

            if (ModelState.IsValid)
            {
                await _context.UpdateAsync(updatedGroup);
                
            }
            return RedirectToAction(nameof(Index));
            //ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", @group.UserId);
            // return View(updatedGroup);
        }

        // GET: Groups/Delete/5
        public IActionResult Delete(Guid id)
        {
            var group = _context.GetByIdAsync(id).Result;
            if (group == null)
            {
                return NotFound();
            }

            return View(group);
        }

        // POST: Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            
            await _context.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
