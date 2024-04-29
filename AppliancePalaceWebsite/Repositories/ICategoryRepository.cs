namespace AppliancePalaceWebsite;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetAll();
    Task<Category?> GetById(int id);
    Task Create(Category product);
    Task Update(Category product);
    Task Delete(Category product);
}