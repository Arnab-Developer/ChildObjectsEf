using ChildObjectsEf.Domain.AggregatesModel.OrderAggregate;

namespace ChildObjectsEf.DomainTest.AggregatesModel.OrderAggregate;

public class OrderAggregateTests
{
    [Fact]
    public void Can_OrderAggregate_CreateOrder()
    {
        // Arrange
        DateTime orderDate = Randomizer<DateTime>.Create();

        // Act
        Order order = new(orderDate);

        // Assert
        Assert.Equal(orderDate, order.OrderDate);
    }

    [Fact]
    public void Can_OrderAggregate_AddItemInOrder()
    {
        // Arrange
        DateTime orderDate = Randomizer<DateTime>.Create();
        string itemName = Randomizer<string>.Create();
        int itemQuantity = Randomizer<int>.Create();

        Order order = new(orderDate);

        // Act
        order.AddItem(itemName, itemQuantity);

        // Assert
        Assert.Equal(orderDate, order.OrderDate);
        Assert.Equal(itemName, order.Items.ElementAt(0).Name);
        Assert.Equal(itemQuantity, order.Items.ElementAt(0).Quantity);
    }

    [Fact]
    public void Can_OrderAggregate_ThroughException_WithNullItemName()
    {
        // Arrange
        DateTime orderDate = Randomizer<DateTime>.Create();

#pragma warning disable CS8600
        string itemName = null;
#pragma warning restore CS8600

        int itemQuantity = Randomizer<int>.Create();
        Order order = new(orderDate);

        // Act
        Assert.Throws<ArgumentNullException>(() => order.AddItem(itemName!, itemQuantity));

        // Assert
        Assert.Equal(orderDate, order.OrderDate);
        Assert.Empty(order.Items);
    }

    [Fact]
    public void Can_OrderAggregate_ThroughException_WithZeroItemQuantity()
    {
        // Arrange
        DateTime orderDate = Randomizer<DateTime>.Create();
        string itemName = Randomizer<string>.Create();
        int itemQuantity = 0;

        Order order = new(orderDate);

        // Act
        Assert.Throws<ArgumentOutOfRangeException>(() => order.AddItem(itemName, itemQuantity));

        // Assert
        Assert.Equal(orderDate, order.OrderDate);
        Assert.Empty(order.Items);
    }

    [Fact]
    public void Can_OrderAggregate_UpdateItemName()
    {
        // Arrange
        DateTime orderDate = Randomizer<DateTime>.Create();
        string itemName = Randomizer<string>.Create();
        int itemQuantity = Randomizer<int>.Create();
        int itemId = Randomizer<int>.Create();

        Order order = new(orderDate);
        order.AddItem(itemName, itemQuantity);

        OrderItem item = order.Items.First(i => i.Name == itemName);
        item.GetType().GetProperty("Id")!.SetValue(item, itemId);

        // Act
        order.UpdateItemName(itemId, "updated name");

        // Assert
        Assert.Equal("updated name", order.Items.ElementAt(0).Name);
    }

    [Fact]
    public void Can_OrderAggregate_UpdateItemQuantity()
    {
        // Arrange
        DateTime orderDate = Randomizer<DateTime>.Create();
        string itemName = Randomizer<string>.Create();
        int itemQuantity = Randomizer<int>.Create();
        int itemId = Randomizer<int>.Create();

        Order order = new(orderDate);
        order.AddItem(itemName, itemQuantity);

        OrderItem item = order.Items.First(i => i.Name == itemName);
        item.GetType().GetProperty("Id")!.SetValue(item, itemId);

        // Act
        order.UpdateItemQuantity(itemId, 105);

        // Assert
        Assert.Equal(105, order.Items.ElementAt(0).Quantity);
    }

    [Fact]
    public void Can_OrderAggregate_RemoveItemFromOrder()
    {
        // Arrange
        DateTime orderDate = Randomizer<DateTime>.Create();
        string itemName = Randomizer<string>.Create();
        int itemQuantity = Randomizer<int>.Create();
        int itemId = Randomizer<int>.Create();

        Order order = new(orderDate);
        order.AddItem(itemName, itemQuantity);

        OrderItem item = order.Items.First(i => i.Name == itemName);
        item.GetType().GetProperty("Id")!.SetValue(item, itemId);

        // Act
        order.RemoveItem(itemId);

        // Assert
        Assert.Empty(order.Items);
    }
}
