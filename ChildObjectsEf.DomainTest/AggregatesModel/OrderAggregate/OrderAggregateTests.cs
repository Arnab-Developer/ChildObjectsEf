using ChildObjectsEf.Domain.AggregatesModel.OrderAggregate;

namespace ChildObjectsEf.DomainTest.AggregatesModel.OrderAggregate;

public class OrderAggregateTests
{
    [Fact]
    public void Can_OrderAggregate_CreateOrder()
    {
        DateTime orderDate = Randomizer<DateTime>.Create();

        Order order = new(orderDate);

        Assert.Equal(orderDate, order.OrderDate);
    }

    [Fact]
    public void Can_OrderAggregate_AddItemInOrder()
    {
        DateTime orderDate = Randomizer<DateTime>.Create();
        string itemName = Randomizer<string>.Create();
        int itemQuantity = Randomizer<int>.Create();

        Order order = new(orderDate);
        order.AddItem(itemName, itemQuantity);

        Assert.Equal(orderDate, order.OrderDate);
        Assert.Equal(itemName, order.Items.ElementAt(0).Name);
        Assert.Equal(itemQuantity, order.Items.ElementAt(0).Quantity);
    }

    [Fact]
    public void Can_OrderAggregate_ThroughException_WithNullItemName()
    {
        DateTime orderDate = Randomizer<DateTime>.Create();

#pragma warning disable CS8600
        string itemName = null;
#pragma warning restore CS8600

        int itemQuantity = Randomizer<int>.Create();

        Order order = new(orderDate);
        Assert.Throws<ArgumentNullException>(() => order.AddItem(itemName!, itemQuantity));

        Assert.Equal(orderDate, order.OrderDate);
        Assert.Empty(order.Items);
    }

    [Fact]
    public void Can_OrderAggregate_ThroughException_WithZeroItemQuantity()
    {
        DateTime orderDate = Randomizer<DateTime>.Create();
        string itemName = Randomizer<string>.Create();
        int itemQuantity = 0;

        Order order = new(orderDate);
        Assert.Throws<ArgumentOutOfRangeException>(() => order.AddItem(itemName, itemQuantity));

        Assert.Equal(orderDate, order.OrderDate);
        Assert.Empty(order.Items);
    }

    [Fact]
    public void Can_OrderAggregate_UpdateItemName()
    {
        DateTime orderDate = Randomizer<DateTime>.Create();
        string itemName = Randomizer<string>.Create();
        int itemQuantity = Randomizer<int>.Create();
        int itemId = Randomizer<int>.Create();

        Order order = new(orderDate);
        order.AddItem(itemName, itemQuantity);

        OrderItem item = order.Items.First(i => i.Name == itemName);
        item.GetType().GetProperty("Id")!.SetValue(item, itemId);
        
        order.UpdateItemName(itemId, "updated name");

        Assert.Equal("updated name", order.Items.ElementAt(0).Name);
    }
}
