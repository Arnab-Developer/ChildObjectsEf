namespace ChildObjectsEf.Domain.DTOs;

public interface IOrderQuery
{
    public Task<Order> GetOrderAsync(int orderId);

    public Task<Order> GetOrderByDateAsync(DateTime orderDate);
}
