using Dapper;
using Microsoft.Data.SqlClient;
using DTOs = ChildObjectsEf.Domain.DTOs;

namespace ChildObjectsEf.Data;

public class OrderQuery : DTOs::IOrderQuery
{
    private readonly string _constr;

    public OrderQuery(string constr)
    {
        _constr = constr;
    }

    async Task<DTOs::Order> DTOs::IOrderQuery.GetOrderAsync(int orderId)
    {
        using SqlConnection con = new(_constr);

        if (con.State != System.Data.ConnectionState.Open)
        {
            await con.OpenAsync();
        }

        const string query =
            @"select Id, OrderDate from Orders where Id = @Id
              select Id, Name, Quantity from OrderItems where OrderId = @Id";

        using SqlMapper.GridReader reader = await con.QueryMultipleAsync(query, new { Id = orderId });
        DTOs::Order order = await reader.ReadSingleAsync<DTOs::Order>();
        order.Items = await reader.ReadAsync<DTOs::OrderItem>();
        return order;
    }

    async Task<IEnumerable<DTOs::Order>> DTOs::IOrderQuery.GetOrderByDateAsync(DateTime orderDate)
    {
        using SqlConnection con = new(_constr);

        if (con.State != System.Data.ConnectionState.Open)
        {
            await con.OpenAsync();
        }

        const string orderQuery = "select Id, OrderDate from Orders where OrderDate = @OrderDate";
        IEnumerable<DTOs::Order> orders = await con.QueryAsync<DTOs::Order>(orderQuery, new { OrderDate = orderDate });

        foreach (DTOs::Order order in orders)
        {
            const string orderItemQuery = "select Id, Name, Quantity from OrderItems where OrderId = @Id";
            order.Items = await con.QueryAsync<DTOs::OrderItem>(orderItemQuery, new { Id = order.Id });
        }

        return orders;
    }
}
