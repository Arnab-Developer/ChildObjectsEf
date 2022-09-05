namespace ChildObjectsEf.Domain.AggregatesModel.OrderAggregate;

public interface IChildObjectsEfRepo : IRepository<Order>
{
    Task<Order> GetOrderAsync(int orderId);

    public ValueTask<int> CreateOrderAsync(Order order);
}