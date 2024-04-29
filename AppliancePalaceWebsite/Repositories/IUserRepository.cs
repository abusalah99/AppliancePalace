namespace AppliancePalaceWebsite;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAll();
    Task<User?> GetById(int id);
    Task<User?> GetByMail(string email);
    Task Add(User product);

}
