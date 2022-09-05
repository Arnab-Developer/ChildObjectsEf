namespace ChildObjectsEf.Domain.DTOs;

public record Order(int Id, DateTime OrderDate, IEnumerable<OrderItem> Items);