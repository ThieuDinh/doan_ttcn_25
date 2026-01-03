using doan_ttcn.Data;
using doan_ttcn.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace doan_ttcn.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator")]
    public class VoucherController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VoucherController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Voucher
        public async Task<IActionResult> Index()
        {
            return View(await _context.Vouchers.ToListAsync());
        }

        // GET: Voucher/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voucher = await _context.Vouchers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (voucher == null)
            {
                return NotFound();
            }

            return View(voucher);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Voucher voucher)
        {
            if(voucher.EndDate<voucher.StartDate)
            {
                ModelState.AddModelError(string.Empty, "End Date must be greater than Start Date");
                return View(voucher);
            }
            bool isCodeExist =await _context.Vouchers.AnyAsync(v=> v.Code==voucher.Code);
            if (isCodeExist)
            {
                ModelState.AddModelError(string.Empty, "Voucher code already exists");
                return View(voucher);
            }
            if (ModelState.IsValid)
            {
                voucher.IsActive=true;
                _context.Add(voucher);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(voucher);
        }
     }
}