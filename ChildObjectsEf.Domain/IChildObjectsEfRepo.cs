namespace ChildObjectsEf.Domain;

public interface IChildObjectsEfRepo
{
    public void CreateOrder(DateTime orderDate);

    public void AddItemInOrder(int orderId, string itemName, int itemQuantity);

    public void UpdateItemNameInOrder(int orderId, int itemId, string itemName);

    public void UpdateItemQuantityInOrder(int orderId, int itemId, int quantity);

    public void RemoveItemFromOrder(int orderId, int itemId);

    public void DeleteOrder(int orderId);
}
