namespace ChildObjectsEf.Application.Queries;

public class GetOrderByDateQuery : IGetOrderByDateQuery
{
    private readonly DTOs::IOrderQuery _orderQuery;

    public GetOrderByDateQuery(DTOs::IOrderQuery orderQuery)
    {
        _orderQuery = orderQuery;
    }

    async Task<DTOs::Order> IGetOrderByDateQuery.GetOrderByDateAsync(DateTime orderDate)
    {
        DTOs::Order order = await _orderQuery.GetOrderByDateAsync(orderDate);
        return order;
    }
}
