namespace ChildObjectsEf.Application.Queries;

public class GetOrderByDateQuery : IGetOrderByDateQuery
{
    private readonly DTOs::IOrderQuery _orderQuery;

    public GetOrderByDateQuery(DTOs::IOrderQuery orderQuery)
    {
        _orderQuery = orderQuery;
    }

    async Task<IEnumerable<DTOs::Order>> IGetOrderByDateQuery.GetOrderByDateAsync(DateTime orderDate)
    {
        IEnumerable<DTOs::Order> orders = await _orderQuery.GetOrderByDateAsync(orderDate);
        return orders;
    }
}
