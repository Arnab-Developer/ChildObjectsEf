using ChildObjectsEf.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ChildObjectsEf.Data;

public class ChildObjectsEfRepo : IChildObjectsEfRepo
{
    private readonly ChildObjectsEfContext _childObjectsEfContext;

    public ChildObjectsEfRepo(ChildObjectsEfContext childObjectsEfContext)
    {
        _childObjectsEfContext = childObjectsEfContext;
    }

    async Task<Order> IChildObjectsEfRepo.GetOrderAsync(int orderId)
    {
        Order order = await _childObjectsEfContext.Orders
            .Include(o => o.Items)
            .FirstAsync(o => o.GetCurrentId() == orderId);

        return order;
    }

    async Task<int> IChildObjectsEfRepo.CreateOrderAsync(Order order)
    {
        EntityEntry<Order> entry = await _childObjectsEfContext.Orders.AddAsync(order);
        Order newOrder = entry.Entity;
        return newOrder.GetCurrentId();
    }

    async Task IChildObjectsEfRepo.SaveAllAsync()
    {
        await _childObjectsEfContext.SaveChangesAsync();
    }
}
