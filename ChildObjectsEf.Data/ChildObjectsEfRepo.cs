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

    void IChildObjectsEfRepo.AddItemInOrder(int orderId, string itemName, int itemQuantity)
    {
        throw new NotImplementedException();
    }

    void IChildObjectsEfRepo.UpdateItemNameInOrder(int orderId, int itemId, string itemName)
    {
        throw new NotImplementedException();
    }

    void IChildObjectsEfRepo.UpdateItemQuantityInOrder(int orderId, int itemId, int quantity)
    {
        throw new NotImplementedException();
    }

    void IChildObjectsEfRepo.RemoveItemFromOrder(int orderId, int itemId)
    {
        throw new NotImplementedException();
    }

    void IChildObjectsEfRepo.DeleteOrder(int orderId)
    {
        throw new NotImplementedException();
    }

    async Task IChildObjectsEfRepo.SaveAllAsync()
    {
        await _childObjectsEfContext.SaveChangesAsync();
    }
}
