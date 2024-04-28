using NUlid;

namespace AppliancePalace;

public class ShoppingCart : BaseEntity
{
    public ICollection<Product> Products { get; set; } = null!;
    public int Quantity { get; set; }
    public Ulid UserId { get; set; }
    public User User { get; set; } = null!;
}