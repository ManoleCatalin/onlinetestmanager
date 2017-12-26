using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Data.Core.Domain;
using Data.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace OTM.Controllers
{
    [Authorize(Roles = "Teacher")]
    [Route("[Controller]/[Action]")]
    public class GroupsController : Controller
    {
        private readonly IGroupsRepository _context;
        private Guid userId;


        public GroupsController(IGroupsRepository context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            userId = new Guid(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
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
        public IActionResult Create(string name,string description)
        {
            var createdGroup = Group.Create(name, description, userId);
            ViewBag.userId = userId;
            if (ModelState.IsValid) //return View(createdGroup);
            _context.InsertAsync(createdGroup);

            return RedirectToAction("Index", "Groups");
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
        public IActionResult Edit(Guid id, string name, string description)
        {
            
            var updatedGroup = _context.GetByIdAsync(id).Result;
            updatedGroup.Update(name,description,userId) ;
           


            if (ModelState.IsValid)
            {
                _context.UpdateAsync(updatedGroup);
                
            }
            return RedirectToAction("Index","Groups");
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
        public IActionResult DeleteConfirmed(Guid id)
        {
            
            _context.DeleteAsync(id);
            return RedirectToAction("Index", "Groups");
        }
    }
}
