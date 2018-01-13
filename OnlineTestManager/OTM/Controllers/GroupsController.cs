﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Constants;
using Microsoft.AspNetCore.Mvc;
using Data.Core.Domain;
using Data.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using OTM.UserContext;
using OTM.ViewModels.Group;

namespace OTM.Controllers
{
    [Authorize(Roles = RoleConstants.TeacherRoleName)]
    [Route("[Controller]/[Action]")]
    public class GroupsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IGroupsRepository _groupsRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly Guid _userId;

        public GroupsController(
            IMapper mapper,
            IGroupsRepository groupsRepository,
            IUserContext userContext, 
            IUsersRepository usersRepository) 
        {
            _mapper = mapper;
            _groupsRepository = groupsRepository;
            _usersRepository = usersRepository;

            var userId = userContext.GetLogedInUserId();
            if (userId == null)
            {
                throw new ApplicationException("userId is null");
            }
            _userId = (Guid)userId;
        }

        public IActionResult Index()
        {
            var groups = _groupsRepository.GetAllGroupsOfTeacherAsync(_userId).Result;
            var groupIndexViewModel = _mapper.Map<IEnumerable<IndexGroupViewModel>>(groups);
            return View(groupIndexViewModel);
        }

        public IActionResult Details(Guid id)
        {
            var group = _groupsRepository.GetByIdAsync(id).Result;
            if (group == null)
            {
                return NotFound();
            }
            var detailsGroupViewModel = _mapper.Map<DetailsGroupViewModel>(group);
            var userGroupsEnumerator = new List<UserGroup>(group.UserGroups);
            var userList = new List<User>();
            foreach (var userGroup in userGroupsEnumerator)
            {
                userList.Add(userGroup.User);
            }

            var studentsList = Mapper.Map<List<DetailsStudentInGroup>>(userList);
            detailsGroupViewModel.Students = studentsList;
            return View(detailsGroupViewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateGroupViewModel createGroupViewModel)
        {
            if (!ModelState.IsValid)
                return View(createGroupViewModel);

            var groupToCreate = Group.Create(createGroupViewModel.Name, 
                                             createGroupViewModel.Description,
                                             _userId);

            await _groupsRepository.InsertAsync(groupToCreate);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(Guid id)
        {
            var group = _groupsRepository.GetByIdAsync(id).Result;
            if (group == null)
            {
                return NotFound();
            }

            var editGroupViewModel = _mapper.Map<EditGroupViewModel>(group);

            var userGroupsEnumerator = new List<UserGroup>(group.UserGroups);
            var userList = new List<User>();
            foreach (var userGroup in userGroupsEnumerator)
            {
                userList.Add(userGroup.User);
            }

            var studentsList = Mapper.Map<List<EditStudentInGroup>>(userList);
    

            editGroupViewModel.Students = studentsList;

            return View(editGroupViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditGroupViewModel editGroupViewModel)
        {
            var updatedGroup = _groupsRepository.GetByIdAsync(editGroupViewModel.Id).Result;
            
            updatedGroup.Update(editGroupViewModel.Name, editGroupViewModel.Description, _userId,false);

            if (ModelState.IsValid)
            {
                await _groupsRepository.UpdateAsync(updatedGroup);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [HttpGet]
        public JsonResult StudentNameAutoComplete(string prefix)
        {
            var students = Mapper.Map<List<EditStudentInGroup>>(_usersRepository.GetStudentsByNamePrefixAsync(prefix).Result);

            return Json(students);
        }

        public IActionResult Delete(Guid id)
        {
            var group = _groupsRepository.GetByIdAsync(id).Result;
            if (group == null)
            {
                return NotFound();
            }
            var deleteGroupViewModel = _mapper.Map<DeleteGroupViewModel>(group);
            return View(deleteGroupViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {

            await _groupsRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [ActionName("RemoveStudentFromGroup")]
        public IActionResult RemoveStudentFromGroup(Guid groupId, Guid studentId)
        {
            var user = _usersRepository.GetByIdAsync(studentId).Result;
            if (user.Id != studentId)
            {
                return NotFound();
            }

            var group = _groupsRepository.GetByIdAsync(groupId).Result;

            var removeStudentFromGroupViewModel = new RemoveStudentFromGroupViewModel{GroupId = groupId,
                StudentId = studentId,
                StudentName = user.UserName,
                GroupName = group.Name
            };

            return View(removeStudentFromGroupViewModel);
        }

        [HttpPost]
        [ActionName("RemoveStudentFromGroup")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveStudentFromGroupConfirmed(RemoveStudentFromGroupViewModel removeStudentFromGroupViewModel)
        {
            var deleted = await _groupsRepository
                .DeleteStudentAsync(removeStudentFromGroupViewModel.GroupId, removeStudentFromGroupViewModel.StudentId);

            if (!deleted)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Edit), new { id = removeStudentFromGroupViewModel.GroupId });
        }

        public IActionResult AddStudentToGroup(Guid id)
        {
            var addStudentToGroup = new AddStudentToGroupViewModel {GroupId = id };
            return View(addStudentToGroup);
        }


        [HttpPost, ActionName("AddStudentToGroup")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddStudentToGroup(AddStudentToGroupViewModel addStudentToGroupViewModel)
        {
            var students =await _usersRepository.GetStudentsByNamePrefixAsync(addStudentToGroupViewModel.StudentName);

        
            if (!ModelState.IsValid)
            {
                return View(addStudentToGroupViewModel);
            }

         

            var inserted = await _groupsRepository
                .InsertStudentAsync(addStudentToGroupViewModel.GroupId, students[0].Id);
            if (!inserted)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(AddStudentToGroup), new {id = addStudentToGroupViewModel.GroupId});
        }
    }
}
