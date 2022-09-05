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

        DTOs::Order order = await con.QueryFirstAsync<DTOs::Order>("select * from Orders where Id = @id", orderId);
        return order;
    }
}
