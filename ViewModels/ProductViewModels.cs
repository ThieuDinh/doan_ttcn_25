namespace doan_ttcn.ViewModels
{
  public class ProductViewModels
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string ImgUrl { get; set; }
    public decimal Price { get; set; }
    public string CategoryName { get; set; }
  }
  public class ProductDetailVM
  {
    public string Name { get; set; }
    public string ImgUrl { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public string CategoryName { get; set; }
    public int Rate { get; set; }

  }
}