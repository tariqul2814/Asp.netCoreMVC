using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UdemyTutorial.Model.ViewModel;

namespace UdemyTutorial.Controllers
{
    [Authorize(Policy ="OnlyAdmin")]
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public RolesController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.Select(
                s=> new RoleVModel
                {
                    Id = s.Id,
                    Name = s.Name
                }).ToListAsync();

            return View(roles);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RoleVModel model)
        {
            if(model==null)
            {
                return NotFound();
            }

            if(await _roleManager.RoleExistsAsync(model.Name))
            {
                ModelState.AddModelError("","Name is exist");
            }

            var role = new IdentityRole()
            {
                Name = model.Name
            };

            await _roleManager.CreateAsync(role);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(string id)
        {
            if(id==null)
            {
                return NotFound();
            }

            var role = await _roleManager.FindByIdAsync(id);
            var model = new RoleVModel
            {
                Name = role.Name,
                Id = role.Id
            };
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = await _roleManager.FindByIdAsync(id);
            var model = new RoleVModel()
            {
                Id = role.Id,
                Name = role.Name
            };
            return View(model);
        }

        [HttpPost, ActionName("Edit")]
        public async Task<IActionResult> EditPost(RoleVModel model)
        {
            if(model==null)
            {
                return NotFound();
            }

            if(await _roleManager.RoleExistsAsync(model.Name))
            {
                ModelState.AddModelError("","Name is Exist");
            }

            var role = await _roleManager.FindByIdAsync(model.Id);
            role.Name = model.Name;
            role.NormalizedName = model.Name.ToUpper();

            await _roleManager.UpdateAsync(role);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            if(id==null)
            {
                return NotFound();
            }

            var role = await _roleManager.FindByIdAsync(id);

            var mode = new RoleVModel
            {
                Id = role.Id,
                Name = role.Name
            };

            return View(mode);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            if(id==null)
            {
                return NotFound();
            }

            var role = await _roleManager.FindByIdAsync(id);
            await _roleManager.DeleteAsync(role);

            return RedirectToAction("Index");
        }
    }
}