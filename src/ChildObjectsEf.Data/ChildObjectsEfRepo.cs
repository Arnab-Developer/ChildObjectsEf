using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ChildObjectsEf.Data;

public class ChildObjectsEfRepo : IChildObjectsEfRepo
{
    private readonly ChildObjectsEfContext _childObjectsEfContext;

    IUnitOfWork IRepository<Order>.UnitOfWork
    {
        get => _childObjectsEfContext;
    }

    public ChildObjectsEfRepo(ChildObjectsEfContext childObjectsEfContext)
    {
        _childObjectsEfContext = childObjectsEfContext;
    }

    async Task<Order> IChildObjectsEfRepo.GetOrderAsync(int orderId)
    {
        Order order = await _childObjectsEfContext.Orders
            .Include(o => o.Items)
            .FirstAsync(o => o.Id == orderId);

        return order;
    }

    async ValueTask<int> IChildObjectsEfRepo.CreateOrderAsync(Order order)
    {
        EntityEntry<Order> entry = await _childObjectsEfContext.Orders.AddAsync(order);
        Order newOrder = entry.Entity;
        return newOrder.Id;
    }

    void IChildObjectsEfRepo.DeleteOrder(Order order)
    {
        _childObjectsEfContext.Orders.Remove(order);
    }
}
