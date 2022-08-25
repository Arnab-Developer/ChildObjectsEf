namespace ChildObjectsEf.Domain;

public class OrderItem
{
    internal int _id;
    private string _name;
    private int _quantity;
    private Order _order;

    public OrderItem(int id, string name, int quantity)
    {
        _id = id;
        _name = name;
        _quantity = quantity;
        _order = new Order(default, default);
    }

    public int GetCurrentId() => _id;

    public string GetCurrentName() => _name;

    public int GetCurrentQuantity() => _quantity;

    internal void ChangeName(string name) => _name = name;

    internal void ChangeQuantity(int quantity) => _quantity = quantity;
}
