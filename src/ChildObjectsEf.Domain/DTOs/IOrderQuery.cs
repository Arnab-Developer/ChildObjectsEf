namespace ChildObjectsEf.Domain.DTOs;

public interface IOrderQuery
{
    public Task<Order> GetOrderAsync(int orderId);

    public Task<IEnumerable<Order>> GetOrderByDateAsync(DateTime orderDate);
}
