namespace ChildObjectsEf.Domain.DTOs;

public record OrderItem
{
    public int Id { get; set; }

    public string Name { get; set; }

    public int Quantity { get; set; }

    public OrderItem()
    {
        Name = string.Empty;
    }
}