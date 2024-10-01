namespace Ecolume.Domain.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public int BrandId { get; set; }
    public Brand Brand { get; set; }
    public string SKU { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    
    public ICollection<Image> Images { get; set; } = new List<Image>();
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
}