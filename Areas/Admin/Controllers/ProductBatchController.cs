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

namespace doan_ttcn.Areas.Admin.Controllers
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


        public async Task<IActionResult> Index()
        {
            var products = await _context.Products
                .Include(p => p.Category)
                .ToListAsync();
            return View(products);
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (product == null) return NotFound();

            var batches = await _context.ProductBatches
                .Where(b => b.ProductId == id)
                .OrderBy(b => b.ExpireDate)
                .ToListAsync();

            ViewBag.Batches = batches;

            return View(product);
        }

        public IActionResult Create(int productId)
        {
            var product = _context.Products.Find(productId);
            if (product == null) return NotFound();

            var batch = new ProductBatch { ProductId = productId };
            ViewBag.ProductName = product.Name;

            return View(batch);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductBatch productBatch)
        {
            if (ModelState.IsValid)
            {
                productBatch.RemainingQuantity = productBatch.Quantity;
                _context.Add(productBatch);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Details), new { id = productBatch.ProductId });
            }

            var product = _context.Products.Find(productBatch.ProductId);
            ViewBag.ProductName = product != null ? product.Name : "Unknown";

            return View(productBatch);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var productBatch = await _context.ProductBatches.FindAsync(id);
            if (productBatch == null) return NotFound();

           
            var product = await _context.Products.FindAsync(productBatch.ProductId);
            ViewBag.ProductName = product?.Name; 

            return View(productBatch);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductBatch productBatch)
        {
            if (id != productBatch.Id) return NotFound();

            
            var batchInDb = await _context.ProductBatches.FindAsync(id);
            if (batchInDb == null) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                
                    batchInDb.ImportDate = productBatch.ImportDate;
                    batchInDb.ExpireDate = productBatch.ExpireDate;

                    int quantityDifference = productBatch.Quantity - batchInDb.Quantity;

                    batchInDb.Quantity = productBatch.Quantity;
                    batchInDb.RemainingQuantity += quantityDifference;

                    if (batchInDb.RemainingQuantity < 0)
                    {
                        ModelState.AddModelError("Quantity", $"Không thể giảm số lượng nhập xuống {productBatch.Quantity} vì đã bán quá số lượng này (Tồn kho sẽ bị âm).");
                        return View(productBatch);
                    }

                    _context.Update(batchInDb);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductBatchExists(productBatch.Id)) return NotFound();
                    else throw;
                }

                return RedirectToAction(nameof(Details), new { id = batchInDb.ProductId });
            }
            return View(productBatch);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var productBatch = await _context.ProductBatches
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productBatch == null) return NotFound();

            return View(productBatch);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productBatch = await _context.ProductBatches.FindAsync(id);
            if (productBatch != null)
            {
                int productId = productBatch.ProductId;
                _context.ProductBatches.Remove(productBatch);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Details), new { id = productId });
            }
            return RedirectToAction(nameof(Index));
        }


        private bool ProductBatchExists(int id)
        {
            return _context.ProductBatches.Any(e => e.Id == id);
        }
    }
}