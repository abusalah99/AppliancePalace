namespace AppliancePalaceWebsite;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAll();
    Task<Product?> GetById(int id);
    Task Create(Product product);
    Task Update(Product product);
    Task Delete(int product);
    Task<IEnumerable<Product>> Filter(string? name = null, string? brand = null, int categoryId = 0);
}
