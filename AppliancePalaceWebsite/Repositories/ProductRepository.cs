
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace AppliancePalaceWebsite;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ProductRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Product>> GetAll()
    {
        return await _dbContext.Products.Include(e => e.Category).ToListAsync();
    }
    public async Task<Product?> GetById(int id)
    {
        return  await _dbContext.Products.Include(e=>e.Category).FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task Create(Product product)
    {
        await _dbContext.Products.AddAsync(product);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Update(Product product)
    {
        Product? productFromDb = await GetById(product.Id);
        if (product == null)
            throw new ArgumentNullException(nameof(product));

        await Task.Run(() => _dbContext.Products.Update(product));
        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        Product? product = await GetById(id);
        if (product == null) 
            throw new ArgumentNullException(nameof(product));

        await Task.Run(() => _dbContext.Products.Remove(product));
        await _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Product>> Filter(string? name = null, string? brand = null, int categoryId = 0)
    {
        IQueryable<Product> query = _dbContext.Products;

        if (!name.IsNullOrEmpty())
            query = query.Where(e=>e.Name.Contains(name!));


        if (!brand.IsNullOrEmpty())
            query = query.Where(e => e.Brand.Contains(brand!));

        if (categoryId > 0)
            query = query.Where(e => e.CategoryId == categoryId);

        return await query.Include(e=>e.Category).ToListAsync();

    }
}

