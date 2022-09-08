namespace ChildObjectsEf.Domain.AggregatesModel.OrderAggregate;

public interface IChildObjectsEfRepo : IRepository<Order>
{
    public Task<Order> GetOrderAsync(int orderId);

    public ValueTask<int> CreateOrderAsync(Order order);

    public void DeleteOrder(Order order);
}