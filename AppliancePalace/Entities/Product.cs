using NUlid;

namespace AppliancePalace;

public class Product : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public double Price { get; set; }
    public int Qunatity { get; set; }
    public string ImagePath { get; set; } = null!;
    public Ulid CategoryId { get; set; }
    public Category Category { get; set; } = null!;
    public Ulid BrandId { get; set; }
    public Brand Brand { get; set; } = null!;
}
