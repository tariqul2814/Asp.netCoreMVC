using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UdemyTutorial.Model.ViewModel;

namespace UdemyTutorial.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserManager<IdentityUser> _userManger;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManger = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userManger.Users.Select(
                    x => new UserVModel
                    {
                        Id = x.Id,
                        Email = x.Email
                    }).ToListAsync();
            return View(users);
        }

        public async Task<IActionResult> Details(string id)
        {
            var user = await _userManger.FindByIdAsync(id);
            var roles = await _roleManager.Roles.ToListAsync();
            var userRoles = (await _userManger.GetRolesAsync(user)).ToList(); 

            var model = new UserVModel()
            {
                Id = user.Id,
                Email = user.Email,
                UserInRoles = roles.Select(s => new RoleVModel()
                {
                    Id = s.Id,
                    Name = s.Name,
                    Select = userRoles.Exists(x => x == s.Name)
                }).ToList()
            };

            return View(model);

        }

        [HttpPost, ActionName("Details")]
        public async Task<IActionResult> Update(string id, UserVModel model)
        {
            var user = await _userManger.FindByIdAsync(id);
            var roles = await _roleManager.Roles.ToListAsync();
            
            foreach(var role in model.UserInRoles)
            {
                if(role.Select)
                {
                    await _userManger.AddToRoleAsync(user, roles.FirstOrDefault(x=> x.Id == role.Id)?.Name);
                }
                else
                {
                    await _userManger.RemoveFromRoleAsync(user, roles.FirstOrDefault(x=> x.Id == role.Id)?.Name);
                }
            }

            return Redirect("Index");
        }

        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManger.FindByIdAsync(id);
            if(user!=null)
            {
                await _userManger.DeleteAsync(user);
            }
            
            return RedirectToAction("Index");
            
        }
    }
}