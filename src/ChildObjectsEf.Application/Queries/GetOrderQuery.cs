namespace ChildObjectsEf.Application.Queries;

public class GetOrderQuery
{
    private readonly DTOs::IOrderQuery _orderQuery;

    public GetOrderQuery(DTOs::IOrderQuery orderQuery)
    {
        _orderQuery = orderQuery;
    }

    async Task<DTOs::Order> GetOrderAsync(int orderId)
    {
        DTOs::Order order = await _orderQuery.GetOrderAsync(orderId);
        return order;
    }
}
