

using doan_ttcn.Models;

namespace doan_ttcn.ViewModels
{
  public class ProductDetailVM
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string ImgUrl { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public string CategoryName { get; set; }
    public int Rate { get; set; }
    public List<Review> Reviews { get; set; } = new List<Review>(); // Danh sách đánh giá
    public double AverageRating { get; set; } = 0; // Điểm trung bình
    public int ReviewCount { get; set; } = 0;
  }
}
