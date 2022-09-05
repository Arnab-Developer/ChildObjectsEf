namespace ChildObjectsEf.Domain.DTOs;

public record Order
{
    public int Id { get; set; }

    public DateTime OrderDate { get; set; }

    public IEnumerable<OrderItem> Items { get; set; }

    public Order()
    {
        Items = new List<OrderItem>();
    }
}