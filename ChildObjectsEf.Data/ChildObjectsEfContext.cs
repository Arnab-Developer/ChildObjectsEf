namespace ChildObjectsEf.Data;

public class ChildObjectsEfContext : DbContext, IUnitOfWork
{
    public DbSet<Order> Orders { get; set; }

    public DbSet<OrderItem> OrderItems { get; set; }

    public ChildObjectsEfContext(DbContextOptions<ChildObjectsEfContext> options)
        : base(options)
    {
        Orders = Set<Order>();
        OrderItems = Set<OrderItem>();
    }

    async Task IUnitOfWork.SaveChangesAsync()
    {
        await SaveChangesAsync();
    }
}