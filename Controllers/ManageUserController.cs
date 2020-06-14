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
        public ActionResult EditRole(string Id)

        {

            IdentityUser model = _bakeryDbContext.Users.Find(Id);

            ViewBag.RoleId = new SelectList(_bakeryDbContext.Roles.ToList().Where(item => model.Id == item.Id).ToList(), "Id", "Name");

            return View(model);

        }


       


    }
}