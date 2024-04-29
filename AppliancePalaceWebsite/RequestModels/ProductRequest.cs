using System.ComponentModel.DataAnnotations;

namespace AppliancePalaceWebsite;

public class ProductRequest
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    public string Description { get; set; } = null!;
    [Required]
    public string Brand { get; set; } = null!;
    [Required]
    public double Price { get; set; }
    [Required]
    public int Qunatity { get; set; }
    [Required]
    public IFormFile Image { get; set; } = null!;
    [Required]
    public int CategoryId { get; set; }
}
public class ProductRequestForUpdate
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
