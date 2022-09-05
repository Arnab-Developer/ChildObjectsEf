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
            @"select o.Id, o.OrderDate, oi.Id, oi.Name, oi.Quantity 
            from Orders o
            inner join OrderItems oi on oi.OrderId = o.Id
            where o.Id = @Id";

        DTOs::Order order = await con.QueryFirstAsync<DTOs::Order>(query, new { Id = orderId });
        return order;
    }
}
