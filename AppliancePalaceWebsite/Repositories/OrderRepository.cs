using Microsoft.EntityFrameworkCore;

namespace AppliancePalaceWebsite;

public class OrderRepository : IOrderRepository
{
    private readonly ApplicationDbContext _context;

    public OrderRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Order?> GetUserCart(int userId)
    {
        Order? order = await _context.Orders.Where(o => o.UserId == userId && o.OrderType == OrderTypeEnum.Cart)
                                            .Include(e => e.ordersProducts)
                                            .ThenInclude(e => e.Product)
                                            .FirstOrDefaultAsync();

        return order;
    }

    public Task Add(Order order)
    {
        throw new NotImplementedException();
    }

    public Task Edit(Order order)
    {
        throw new NotImplementedException();
    }
}
