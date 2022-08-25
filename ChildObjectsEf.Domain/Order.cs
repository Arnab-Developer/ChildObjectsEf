namespace ChildObjectsEf.Domain;

public class Order
{
    private int _id;
    private DateTime _orderDate;
    private List<OrderItem> _items;

    public IEnumerable<OrderItem> Items { get => _items.AsEnumerable(); }

    public Order(DateTime orderDate)
    {
        _orderDate = orderDate;
        _items = new List<OrderItem>();
    }

    public int GetCurrentId() => _id;

    public DateTime GetCurrentDate() => _orderDate;

    public void AddItem(string name, int quantity)
    {
        OrderItem orderItem = new(name, quantity);
        _items.Add(orderItem);
    }

    public void UpdateItemName(int itemId, string name)
    {
        OrderItem oi = GetItem(itemId);
        oi.ChangeName(name);
    }

    public void UpdateItemQuantity(int itemId, int quantity)
    {
        OrderItem oi = GetItem(itemId);
        oi.ChangeQuantity(quantity);
    }

    public void RemoveItem(int itemId)
    {
        OrderItem oi = GetItem(itemId);
        _items.Remove(oi);
    }

    private OrderItem GetItem(int itemId) => _items.First(i => i.GetCurrentId() == itemId);
}