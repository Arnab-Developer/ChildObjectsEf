namespace ChildObjectsEf.Application.Queries;

public interface IGetOrderByDateQuery
{
    public Task<IEnumerable<DTOs::Order>> GetOrderByDateAsync(DateTime orderDate);
}
