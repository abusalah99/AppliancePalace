namespace AppliancePalace;

public class ProductRequest
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Brand { get; set; } = null!;
    public double Price { get; set; }
    public int Qunatity { get; set; }
    public IFormFile Image { get; set; } = null!;
    public int CategoryId { get; set; }
}
