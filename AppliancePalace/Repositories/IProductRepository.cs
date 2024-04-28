namespace AppliancePalace;

public interface IProductRepository
{
    Task<IEnumerator<Product>> GetAll();
    Task<Product> Get(int id);
    Task Create(int id);

}

