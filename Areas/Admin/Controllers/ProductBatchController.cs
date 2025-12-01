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

namespace doan_ttcn.Areas_Admin_Controllers
{
      [Area("Admin")]
    [Authorize(Roles = "Administrator,Manager")]
    public class ProductBatchController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductBatchController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProductBatch
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ProductBatches.Include(p => p.Product);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ProductBatch/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productBatch = await _context.ProductBatches
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productBatch == null)
            {
                return NotFound();
            }

            return View(productBatch);
        }

        // GET: ProductBatch/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Description");
            return View();
        }

        // POST: ProductBatch/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ImportDate,ExpireDate,Quantity,RemainingQuantity,ProductId")] ProductBatch productBatch)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productBatch);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Description", productBatch.ProductId);
            return View(productBatch);
        }

        // GET: ProductBatch/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productBatch = await _context.ProductBatches.FindAsync(id);
            if (productBatch == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Description", productBatch.ProductId);
            return View(productBatch);
        }

        // POST: ProductBatch/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ImportDate,ExpireDate,Quantity,RemainingQuantity,ProductId")] ProductBatch productBatch)
        {
            if (id != productBatch.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productBatch);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductBatchExists(productBatch.Id))
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
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Description", productBatch.ProductId);
            return View(productBatch);
        }

        // GET: ProductBatch/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productBatch = await _context.ProductBatches
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productBatch == null)
            {
                return NotFound();
            }

            return View(productBatch);
        }

        // POST: ProductBatch/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productBatch = await _context.ProductBatches.FindAsync(id);
            if (productBatch != null)
            {
                _context.ProductBatches.Remove(productBatch);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductBatchExists(int id)
        {
            return _context.ProductBatches.Any(e => e.Id == id);
        }
    }
}
