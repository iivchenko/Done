using System;
using System.Linq;
using System.Threading.Tasks;
using Done.Core.Data;
using Done.Core.Web.Pagination;
using Done.Web.Areas.Administration.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Done.Web.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Authorize(Roles = "Administrators")]
    public sealed class UserManagementController : Controller
    {

        private const int PageSize = 20;
        private const int PagesCount = 5;
        private readonly UserManager<IdentityUser> _users;
        private readonly RoleManager<IdentityRole> _roles;

        public UserManagementController(UserManager<IdentityUser> users, RoleManager<IdentityRole> roles)
        {
           _users = users;
           _roles = roles;
        }

        public async Task<IActionResult> Index(string pattern = "", int page = 1)
        {
            var usersCount = _users.Users.Count(x => x.UserName.Contains(pattern));
            var totalPages = (int)Math.Ceiling(usersCount / (decimal)PageSize);

            if (page < 1 || (page > totalPages && totalPages > 0))
            {
                return NotFound($"Invalid page '{page}'! Should be in range from 1 to {totalPages}");
            }

            var users =
                await _users
                    .Users
                    .Where(x => x.UserName.Contains(pattern))
                    .OrderBy(x => x.Id)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize)
                    .ToListAsync();

            var indexVm = new IndexViewModel
            {
                Total = usersCount,
                Pagination = new PageViewModel(totalPages, PagesCount, page),
                //Users = users.Select(x => x.ToViewModel()), // TODO: need a view model here
                Users = users,
                Pattern = pattern
            };

            return View("Index", indexVm);
        }

        [HttpGet]
        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> New([Bind("Name", "Password", "ConfirmPassword", "Role")] NewUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var newUser = new IdentityUser 
                {
                    UserName = model.Name,
                };
                
                var result = await _users.CreateAsync(newUser, model.Password);
                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(", ", result.Errors.Select(x => x.Description)));
                }

                result = await _users.AddToRoleAsync(newUser, model.Role);
                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(", ", result.Errors.Select(x => x.Description)));
                }                

                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(string id)
        {
            // TODO: Provide correct and nice error view when item is not exist
            var user = await _users.FindByIdAsync(id);
            var model = new EditUserViewModel
            {
                Id = user.Id,
                Name = user.UserName,
                Role = (await _users.GetRolesAsync(user)).First()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind("Id", "Name", "NewPassword", "ConfirmPassword", "Role")] EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _users.FindByIdAsync(model.Id);
                user.UserName = model.Name;
                await _users.UpdateAsync(user);

                if (!string.IsNullOrWhiteSpace(model.NewPassword))
                {
                    await _users.RemovePasswordAsync(user);
                    await _users.AddPasswordAsync(user, model.NewPassword);
                }

                var role = await _users.GetRolesAsync(user);
                await _users.RemoveFromRoleAsync(user, role.First());
                await _users.AddToRoleAsync(user, model.Role);

                return RedirectToAction("Index");
            }

            return View(model);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _users.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}