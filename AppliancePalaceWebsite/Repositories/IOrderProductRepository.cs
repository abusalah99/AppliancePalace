namespace AppliancePalaceWebsite;

public interface IOrderProductRepository
{
    Task Add(OrderProduct orderProducr); 
    Task Edit(OrderProduct orderProduct);
}
