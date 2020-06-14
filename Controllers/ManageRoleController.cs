using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyBakeryShop.Models.Data;

namespace MyBakeryShop.Controllers
{
    public class ManageRoleController : Controller
    {
        private readonly BakeryDbContext _bakeryDbContext;
        public ManageRoleController(BakeryDbContext bakeryDbContext)
        {
            _bakeryDbContext = bakeryDbContext;
        }
        public async Task<IActionResult> Index()
        {
            var model = _bakeryDbContext.Roles.AsAsyncEnumerable();
            return View(model);
        }
        public ViewResult Create()

        {

            return View();

        }

        [HttpPost]

        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create([Bind("Id,Name")] IdentityRole role)

        {

            try

            {

                if (ModelState.IsValid)

                {

                    _bakeryDbContext.Roles.Add(role);

                    _bakeryDbContext.SaveChanges();

                }

                return RedirectToAction("Index");

            }

            catch (Exception ex)

            {

                ModelState.AddModelError("", ex.Message);

            }

            return View(role);

        }
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var banner =  _bakeryDbContext.Roles
                .Find(id);
            if (banner == null)
            {
                return NotFound();
            }

            return View(banner);
        }

        // POST: AdminBanners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var banner = await _bakeryDbContext.Roles.FindAsync(id);
            _bakeryDbContext.Roles.Remove(banner);
            await _bakeryDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}