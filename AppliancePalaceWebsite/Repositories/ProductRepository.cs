
using AppliancePalaceWebsite.RequestModels;
using Microsoft.EntityFrameworkCore;

namespace AppliancePalaceWebsite;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ProductRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<Product> GetAll()
    {
        return _dbContext.Products.ToList();
    }
    public Product? GetById(int id)
    {
        return _dbContext.Products.FirstOrDefault(p => p.Id == id);
    }

    public void Create(Product product)
    {
        _dbContext.Products.Add(product);
        _dbContext.SaveChanges();
    }

    public void Update(Product product)
    {
        _dbContext.Products.Remove(product);
        _dbContext.SaveChanges();
    }

    public void Delete(int id)
    {
        Product? productFromDb = GetById(id);

        _dbContext.Products.Remove(productFromDb);
        _dbContext.SaveChanges();
    }
}
