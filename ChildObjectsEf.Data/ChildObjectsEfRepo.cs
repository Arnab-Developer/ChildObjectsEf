using ChildObjectsEf.Domain;

namespace ChildObjectsEf.Data;

public class ChildObjectsEfRepo : IChildObjectsEfRepo
{
    void IChildObjectsEfRepo.CreateOrder(DateTime orderDate)
    {
        throw new NotImplementedException();
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
}
