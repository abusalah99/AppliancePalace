using Microsoft.EntityFrameworkCore;

namespace AppliancePalaceWebsite;

public class OrderProductRepository : IOrderProductRepository
{
    private readonly ApplicationDbContext _dbContext;

    public OrderProductRepository(ApplicationDbContext context)
    {
        _dbContext = context;
    }

    public async Task Add(OrderProduct orderProducr)
    {
        await _dbContext.OrderProducts.AddAsync(orderProducr);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Edit(OrderProduct orderProducr)
    {
        await Task.Run(() => _dbContext.OrderProducts.Update(orderProducr));
        await _dbContext.SaveChangesAsync();
    }
}