namespace ChildObjectsEf.Data;

public class ChildObjectsEfContext : DbContext
{
    public DbSet<Order> Orders { get; set; }

    public DbSet<OrderItem> OrderItems { get; set; }

    public ChildObjectsEfContext(DbContextOptions<ChildObjectsEfContext> options)
        : base(options)
    {
        Orders = Set<Order>();
        OrderItems = Set<OrderItem>();
    }
}