namespace ChildObjectsEf.Domain;

public interface IChildObjectsEfRepo
{
    Task<Order> GetOrderAsync(int orderId);

    public ValueTask<int> CreateOrderAsync(Order order);

    public Task SaveAllAsync();
}
