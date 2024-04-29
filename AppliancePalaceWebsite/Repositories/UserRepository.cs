using Microsoft.EntityFrameworkCore;

namespace AppliancePalaceWebsite;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _dbContext;

    public UserRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<IEnumerable<User>> GetAll()
    {
        return await _dbContext.Users.ToListAsync();
    }
    public async Task<User?> GetById(int id)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
    }
    public async Task<User?> GetByMail(string email)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
    }
    public async Task Add(User category)
    {
        await _dbContext.Users.AddAsync(category);
        await _dbContext.SaveChangesAsync();
    }
}
