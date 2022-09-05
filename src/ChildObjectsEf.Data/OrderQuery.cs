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
}
