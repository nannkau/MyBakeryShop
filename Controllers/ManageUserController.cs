using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp.Dom;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyBakeryShop.Models.Data;
using MyBakeryShop.ViewModels;

namespace MyBakeryShop.Controllers
{
    public class ManageUserController : Controller
    {
        private readonly BakeryDbContext _bakeryDbContext;
        public ManageUserController(BakeryDbContext bakeryDbContext)
        {
            _bakeryDbContext = bakeryDbContext;
        }
        public IActionResult Index()
        {
            IEnumerable<IdentityUser> model = _bakeryDbContext.Users.AsEnumerable();

            return View(model);
        }
        public ActionResult Edit(string Id)

        {

            IdentityUser model = _bakeryDbContext.Users.Find(Id);

            return View(model);

        }
        [HttpPost]

        [ValidateAntiForgeryToken]

        public ActionResult Edit(IdentityUser model)

        {

            try

            {

                _bakeryDbContext.Entry(model).State = EntityState.Modified;

                _bakeryDbContext.SaveChanges();

                return RedirectToAction("Index");

            }

            catch (Exception ex)

            {

                ModelState.AddModelError("", ex.Message);

                return View(model);

            }

        }
        public async Task<IActionResult> EditRole(string Id)

        {

            var model = await _bakeryDbContext.UserRoles.FindAsync(Id);
            return View(model);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ManageUserRoles(string [] model, string userId)
        {
            var user = await _bakeryDbContext.UserRoles.FindAsync(userId);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {userId} cannot be found";
                return View("NotFound");
            }

              _bakeryDbContext.UserRoles.Remove(user);

           
            foreach(string r in model ){
                IdentityUserRole<string> a = new IdentityUserRole<string>();
                a.RoleId = r;
                a.UserId = userId;
                _bakeryDbContext.UserRoles.Add(a);
            }
            ViewBag.RoleId = new SelectList(_bakeryDbContext.Roles.ToList().Where(item => item.Id == user.RoleId).ToList(), "Id", "Name");

            return RedirectToAction("EditUser", new { Id = userId });
        }
        [HttpPost]

        [ValidateAntiForgeryToken]

        public async Task<IActionResult> DeleteRoleFromUser(string UserId, string RoleId)

        {

            var user = await _bakeryDbContext.UserRoles.FindAsync(UserId);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {UserId} cannot be found";
                return View("NotFound");
            }

            _bakeryDbContext.UserRoles.Remove(user);
            _bakeryDbContext.SaveChanges();

            ViewBag.RoleId = new SelectList(_bakeryDbContext.Roles.ToList().Where(item => item.Id==user.RoleId).ToList(), "Id", "Name");

            return RedirectToAction("EditRole", new { Id = UserId });

        }




    }
}