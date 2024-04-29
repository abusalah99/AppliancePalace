namespace AppliancePalaceWebsite;

public interface IOrderRepository
{
    Task<Order?> GetUserCart(int userId);
    Task Add(Order order);
    Task Edit(Order order);
}
