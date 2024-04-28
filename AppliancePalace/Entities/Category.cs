using NUlid;

namespace AppliancePalace;

public class Category
{
    public Ulid Id { get; set; }
    public string Name { get; set; } = null!;
    public ICollection<Product>? Products { get; set; }
}
