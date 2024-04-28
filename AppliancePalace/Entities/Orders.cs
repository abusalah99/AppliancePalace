using NUlid;

namespace AppliancePalace;

public class Orders : BaseEntity
{
    public ICollection<Product> Products { get; set; } = null!;
    public int Quantity { get; set; }
    public double TotalPrice {  get; set; }
    public OrderType OrderType { get; set; }
    public Ulid UserId { get; set; }
    public User User { get; set; } = null!;

}