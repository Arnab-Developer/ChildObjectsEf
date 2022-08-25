namespace ChildObjectsEf.Domain;

public class OrderItem
{
    public int Id { get; private set; }

    public string Name { get; internal set; }

    public int Quantity { get; internal set; }

    public Order Order { get; internal set; }

    public OrderItem(string name, int quantity)
    {
        Name = name;
        Quantity = quantity;
        Order = new Order(default);
    }
}
