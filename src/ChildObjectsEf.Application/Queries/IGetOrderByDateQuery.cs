namespace ChildObjectsEf.Application.Queries;

public interface IGetOrderByDateQuery
{
    public Task<DTOs::Order> GetOrderByDateAsync(DateTime orderDate);
}
