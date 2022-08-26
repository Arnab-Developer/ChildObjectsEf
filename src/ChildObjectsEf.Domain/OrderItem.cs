namespace ChildObjectsEf.Domain;

public class OrderItem
{
    private string _name;
    private int _quantity;

    public int Id { get; internal set; }

    public string Name 
    { 
        get => _name;
        internal set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(nameof(Name));
            }
            _name = value;
        }
    }

    public int Quantity
    {
        get => _quantity;
        internal set
        {
            if (value <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(Quantity));
            }
            _quantity = value;
        }
    }

    public Order Order { get; internal set; }

    public OrderItem(string name, int quantity)
    {
        _name = string.Empty;

        Name = name;
        Quantity = quantity;
        Order = new Order(default);
    }
}
