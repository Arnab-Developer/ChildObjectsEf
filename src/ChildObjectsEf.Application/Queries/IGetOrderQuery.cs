namespace ChildObjectsEf.Application.Queries;

public interface IGetOrderQuery
{
    public Task<DTOs::Order> GetOrderAsync(int orderId);
}