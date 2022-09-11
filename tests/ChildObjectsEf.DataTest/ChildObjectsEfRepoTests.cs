namespace ChildObjectsEf.DataTest;

public class ChildObjectsEfRepoTests
{
    [Fact]
    public async Task Can_GetOrderAsync_ReturnProperData()
    {
        // Arrange
        DateTime orderDateTime = Randomizer<DateTime>.Create();
        int orderId = Randomizer<int>.Create();

        Order order = new(orderDateTime);
        order.AddItem("item1", 10);
        order.AddItem("item2", 20);

        order.GetType().GetProperty("Id")!.SetValue(order, orderId);

        OrderItem item = order.Items.First(i => i.Name == "item1");
        item.GetType().GetProperty("Id")!.SetValue(item, 1);

        item = order.Items.First(i => i.Name == "item2");
        item.GetType().GetProperty("Id")!.SetValue(item, 2);

        DbContextOptions<ChildObjectsEfContext> arrangeOptions = new DbContextOptionsBuilder<ChildObjectsEfContext>()
            .UseInMemoryDatabase("ChildObjectsEfDb")
            .Options;

        ChildObjectsEfContext arrangeContext = new(arrangeOptions);

        await arrangeContext.Orders.AddAsync(order);
        await arrangeContext.SaveChangesAsync();

        DbContextOptions<ChildObjectsEfContext> options = new DbContextOptionsBuilder<ChildObjectsEfContext>()
            .UseInMemoryDatabase("ChildObjectsEfDb")
            .Options;

        ChildObjectsEfContext context = new(options);
        IChildObjectsEfRepo childObjectsEfRepo = new ChildObjectsEfRepo(context);

        // Act
        Order dbOrder = await childObjectsEfRepo.GetOrderAsync(orderId);

        // Assert
        Assert.Equal(orderId, dbOrder.Id);
        Assert.Equal(orderDateTime, dbOrder.OrderDate);

        Assert.Equal("item1", dbOrder.Items.First(i => i.Id == 1).Name);
        Assert.Equal(10, dbOrder.Items.First(i => i.Id == 1).Quantity);

        Assert.Equal("item2", dbOrder.Items.First(i => i.Id == 2).Name);
        Assert.Equal(20, dbOrder.Items.First(i => i.Id == 2).Quantity);
    }
}