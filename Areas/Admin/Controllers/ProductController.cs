using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using doan_ttcn.Data;
using doan_ttcn.Models;
using Microsoft.AspNetCore.Authorization;
using Areas.Admin.ViewModel;

namespace doan_ttcn.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator,Manager")]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Product
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Products.Include(p => p.Category);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Product/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Product/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }

        // POST: Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCreateVM model)
        {

            if (ModelState.IsValid)
            {
                string uniqueFileName = "default.jpg";
                if (model.Photo != null)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "product");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.Photo.CopyToAsync(fileStream);
                    }
                }
                var product = new Product
                {
                    Name = model.Name,
                    Price = model.Price,
                    Description = model.Description,
                    CategoryId = model.CategoryId,
                    IsActive = true,
                    ImageUrl = uniqueFileName,
                    CategoryName = _context.Categories.FirstOrDefault(c => c.Id == model.CategoryId)?.Name
                };
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", model.CategoryId);
            return View(model);
        }

        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
            return View(product);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Price,ImageUrl,IsActive,CategoryId")] Product product, IFormFile? imageFile)
        {
            if (id != product.Id)
            {
                return NotFound();
            }
            ModelState.Remove("ImageUrl");
            ModelState.Remove("Category");
            if (ModelState.IsValid)
            {
                try
                {
                    if (imageFile != null && imageFile.Length > 0)
                    {
                        // 1. Upload ảnh mới
                        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                        var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "product");

                        if (!Directory.Exists(uploadPath)) Directory.CreateDirectory(uploadPath);

                        var filePath = Path.Combine(uploadPath, fileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await imageFile.CopyToAsync(stream);
                        }

                        // 2. Cập nhật tên ảnh mới vào Model
                        product.ImageUrl = fileName;

                    }

                    product.CategoryName = _context.Categories.FirstOrDefault(c => c.Id == product.CategoryId)?.Name;
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
            return View(product);
        }

        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
        public async Task<IActionResult> Enabled(string? id)
        {
            if (id == null) return NotFound();
            var pro = await _context.Products.FindAsync(int.Parse(id));
            if (pro == null) return NotFound();

            pro.IsActive = true;
            _context.Update(pro);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Disabled(string? id)
        {
            if (id == null) return NotFound();
            var pro =  await _context.Products.FindAsync(int.Parse(id));
            if (pro == null) return NotFound();

            pro.IsActive = false;
            _context.Update(pro);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}