namespace ChildObjectsEf.Domain;

public class Order
{
    private List<OrderItem> _items;

    public int Id { get; private set; }

    public DateTime OrderDate { get; set; }    

    public IEnumerable<OrderItem> Items { get => _items.AsEnumerable(); }

    public Order(int id, DateTime orderDate)
    {
        Id = id;
        OrderDate = orderDate;
        _items = new List<OrderItem>();
    }

    public void AddItem(int id, string name, int quantity)
    {
        OrderItem orderItem = new(id, name, quantity);
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