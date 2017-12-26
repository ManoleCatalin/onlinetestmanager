using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Data.Core.Domain;
using Data.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OTM.Models;

namespace OTM.Components
{
    [ViewComponent(Name = "Navbar")]
    public class NavbarViewComponent : ViewComponent
    {
        private readonly IUsersRepository _usersRepository;
        private readonly string _userId;

        public NavbarViewComponent(IUsersRepository usersRepository, IHttpContextAccessor httpContextAccessor)
        {
            _usersRepository = usersRepository;

            _userId = httpContextAccessor.HttpContext.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            User user = null;

            if (_userId != null)
            {
                user = await _usersRepository.GetByIdAsync(Guid.Parse(_userId));
            }       

            var model = new NavbarViewModel()
            {
                UserType = user?.UserType
            };

            return View(model);
        }
    }


}
