using System.Text.Json.Serialization;

namespace AppliancePalaceWebsite;

public class Order
{
    public int Id { get; set; }
    public double TotalPrice {  get; set; }
    public OrderType OrderType { get; set; }
    [JsonIgnore]
    public int UserId { get; set; }
    [JsonIgnore]
    public User User { get; set; } = null!;
    public ICollection<OrderProduct> ordersProducts { get; set; } = null!;

}