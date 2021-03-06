﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyBakeryShop.Models;
using MyBakeryShop.Models.Data;

using System.IO;
using Microsoft.AspNetCore.Hosting;
using MyBakeryShop.ViewModels;

namespace MyBakeryShop.Controllers
{
    public class AdminProductsController : Controller
    {
        private readonly BakeryDbContext _context;
        private readonly IProductRepository _productRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AdminProductsController(BakeryDbContext context, IWebHostEnvironment webHostEnvironment, IProductRepository productRepository)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _productRepository = productRepository;
        }
        private string UploadedFile( CreateProductVm model)  
        {  
            string uniqueFileName = null;  
  
            if (model.ProfileImage != null)  
            {  
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");  
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ProfileImage.FileName;  
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);  
                using (var fileStream = new FileStream(filePath, FileMode.Create))  
                {  
                    model.ProfileImage.CopyTo(fileStream);  
                }  
            } 
            
            return "~/images/" +uniqueFileName;  
        }  
        // GET: AdminProducts
        public async Task<IActionResult> Index(string searchString,string styleString)
        {
            ProductListViewModel piesListViewModel = new ProductListViewModel();
            piesListViewModel.Style = new SelectList(_productRepository.StyleList().ToList());
            piesListViewModel.Products = _productRepository.SearchListAd(searchString, styleString);


            return View(piesListViewModel);
        }

        // GET: AdminProducts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: AdminProducts/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId");
            return View();
        }

        // POST: AdminProducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,Name,ShortDescription,LongDescription,AllergyInformation,Price,IsProductOfTheWeek,InStock,CategoryId,ProfileImage")] CreateProductVm product)
        {
            string ulr = UploadedFile(product);
            if (ModelState.IsValid)
            {
                Product p = new Product
                {
                    ProductId = product.ProductId,
                    Name = product.Name,
                    ShortDescription = product.ShortDescription,
                    LongDescription = product.LongDescription,
                    AllergyInformation = product.AllergyInformation,
                    Price = product.Price,
                    IsProductOfTheWeek = product.IsProductOfTheWeek,
                    InStock = product.InStock,
                    CategoryId = product.CategoryId,
                    ImageThumbnailUrl = ulr,
                    ImageUrl = ulr,

                };
                _context.Add(p);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", product.CategoryId);
            return View(product);
        }

        // GET: AdminProducts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);

            CreateProductVm p = new CreateProductVm
            {
                ProductId = product.ProductId,
                Name = product.Name,
                ShortDescription = product.ShortDescription,
                LongDescription = product.LongDescription,
                AllergyInformation = product.AllergyInformation,
                Price = product.Price,
                IsProductOfTheWeek = product.IsProductOfTheWeek,
                InStock = product.InStock,
                CategoryId = product.CategoryId,
            };
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", product.CategoryId);
            return View(p);
        }

        // POST: AdminProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,Name,ShortDescription,LongDescription,AllergyInformation,Price,IsProductOfTheWeek,InStock,CategoryId,ProfileImage")] CreateProductVm product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }
            string ulr = UploadedFile(product);
            if (ModelState.IsValid)
            {
                Product p = new Product
                {
                    ProductId = product.ProductId,
                    Name = product.Name,
                    ShortDescription = product.ShortDescription,
                    LongDescription = product.LongDescription,
                    AllergyInformation = product.AllergyInformation,
                    Price = product.Price,
                    IsProductOfTheWeek = product.IsProductOfTheWeek,
                    InStock = product.InStock,
                    CategoryId = product.CategoryId,
                    ImageThumbnailUrl = ulr,
                    ImageUrl = ulr,

                };
                try
                {
                    _context.Update(p);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", product.CategoryId);
            return View();
        }
        // GET: AdminProducts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: AdminProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
