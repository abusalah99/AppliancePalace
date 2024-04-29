
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
        return await _dbContext.Categories.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task Create(Category category)
    {
        await _dbContext.Categories.AddAsync(category);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Update(Category category)
    {
        await Task.Run(() => _dbContext.Categories.Update(category));
        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(Category category)
    {
        await Task.Run(() => _dbContext.Categories.Remove(category));
        await _dbContext.SaveChangesAsync();
    }
}

