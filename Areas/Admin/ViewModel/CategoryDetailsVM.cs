using doan_ttcn.Models;

namespace doan_ttcn.Areas.Admin.ViewModel
{
    public class CategoryDeatilsVM
    {
        public Category category{ get; set; }
        public List<Product> products { get; set; }
    }
}