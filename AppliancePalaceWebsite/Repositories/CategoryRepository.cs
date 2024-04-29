
using Microsoft.EntityFrameworkCore;

namespace AppliancePalaceWebsite;

public class CategoryRepository : ICategoryRepository
{
    private readonly ApplicationDbContext _dbContext;

    public CategoryRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Category>> GetAll()
    {
        return await _dbContext.Categories.ToListAsync();
    }
    public async Task<Category?> GetById(int id)
    {
        return await _dbContext.Categories.FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task Add(Category category)
    {
        await _dbContext.Categories.AddAsync(category);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Edit(Category category)
    {
        Category? categoryFromDb = await GetById(category.Id);
        if (categoryFromDb == null)
            throw new ArgumentNullException(nameof(category));

        await Task.Run(() => _dbContext.Categories.Update(category));
        await _dbContext.SaveChangesAsync();
    }

    public async Task Remove(int id)
    {
        Category? categoryFromDb = await GetById(id);
        if (categoryFromDb == null)
            throw new ArgumentNullException(nameof(categoryFromDb));

        await Task.Run(() => _dbContext.Categories.Remove(categoryFromDb));
        await _dbContext.SaveChangesAsync();
    }
}

