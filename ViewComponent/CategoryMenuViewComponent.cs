using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using doan_ttcn.Data; // Nhớ using namespace của DbContext

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
            // Tự đi chợ lấy danh mục
            var categories =  _context.Categories.Select(cate => new MenuCategory
            {
                Id = cate.Id,
                Name = cate.Name,
                ProductCount = cate.Products.Count()
            });
            
            // Trả về View riêng của nó kèm dữ liệu
            return View(categories);
        }
    }
}