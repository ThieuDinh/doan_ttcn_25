using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using doan_ttcn.Data;
using doan_ttcn.ViewModels; // Nhớ using namespace của DbContext

namespace doan_ttcn.ViewComponents
{
    public class CategoryMenuViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public CategoryMenuViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories =  _context.Categories.Select(cate => new MenuCategoryVM
            {
                Id = cate.Id,
                Name = cate.Name,
                ProductCount = cate.Products.Count()
            });
            
        
            return View(categories);
        }
    }
}