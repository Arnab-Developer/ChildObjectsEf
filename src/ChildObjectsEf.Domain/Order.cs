namespace ChildObjectsEf.Domain;

public class Order
{
    private readonly List<OrderItem> _items;

    public int Id { get; private set; }

    public DateTime OrderDate { get; private set; }    

    public IEnumerable<OrderItem> Items { get => _items.AsEnumerable(); }

    public Order(DateTime orderDate)
    {
        OrderDate = orderDate;
        _items = new List<OrderItem>();
    }

    public void AddItem(string name, int quantity)
    {
        OrderItem orderItem = new(name, quantity);
        _items.Add(orderItem);
    }

    public void UpdateItemName(int itemId, string name)
    {
        OrderItem oi = GetItem(itemId);
        oi.Name = name;
    }

    public void UpdateItemQuantity(int itemId, int quantity)
    {
        OrderItem oi = GetItem(itemId);
        oi.Quantity = quantity;
    }

    public void RemoveItem(int itemId)
    {
        OrderItem oi = GetItem(itemId);
        _items.Remove(oi);
    }

    private OrderItem GetItem(int itemId) => _items.First(i => i.Id == itemId);
}