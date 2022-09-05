namespace ChildObjectsEf.ApiTest.Handlers;

public class Func1CommandHandlerHandlerTests
{
    [Fact]
    public async Task Can_Func1CommandHandlerHandler_UpdateOrderAndItem()
    {
        // Arrange
        DateTime orderDateTime = Randomizer<DateTime>.Create();
        int orderId = Randomizer<int>.Create();

        Mock<IChildObjectsEfRepo> childObjectsEfRepoMock = new();
        Func1Command func1Command = new(orderId, orderDateTime, 2, "updated name", 18, 4);
        CancellationToken cancellationToken = new();

        IRequestHandler<Func1Command, bool> requestHandler =
            new Func1CommandHandler(childObjectsEfRepoMock.Object);

        Order order = new(orderDateTime);
        order.AddItem("item1", 10);
        order.AddItem("item2", 20);
        order.AddItem("item3", 14);
        order.AddItem("item4", 108);

        order.GetType().GetProperty("Id")!.SetValue(order, orderId);

        OrderItem item = order.Items.First(i => i.Name == "item1");
        item.GetType().GetProperty("Id")!.SetValue(item, 1);

        item = order.Items.First(i => i.Name == "item2");
        item.GetType().GetProperty("Id")!.SetValue(item, 2);

        item = order.Items.First(i => i.Name == "item3");
        item.GetType().GetProperty("Id")!.SetValue(item, 3);

        item = order.Items.First(i => i.Name == "item4");
        item.GetType().GetProperty("Id")!.SetValue(item, 4);

        childObjectsEfRepoMock
            .Setup(s => s.GetOrderAsync(orderId))
            .ReturnsAsync(order);

        // Act
        bool isSuccess = await requestHandler.Handle(func1Command, cancellationToken);

        // Assert
        childObjectsEfRepoMock
            .Verify(v => v.GetOrderAsync(orderId),
                Times.Once);

        childObjectsEfRepoMock
            .Verify(v => v.SaveAllAsync(),
                Times.Once);

        childObjectsEfRepoMock.VerifyNoOtherCalls();

        Assert.True(isSuccess);

        Assert.Equal(orderId, order.Id);
        Assert.Equal(orderDateTime, order.OrderDate);

        Assert.Equal("item1", order.Items.First(i => i.Id == 1).Name);
        Assert.Equal(10, order.Items.First(i => i.Id == 1).Quantity);

        Assert.Equal("updated name", order.Items.First(i => i.Id == 2).Name);
        Assert.Equal(18, order.Items.First(i => i.Id == 2).Quantity);

        Assert.Equal("item3", order.Items.First(i => i.Id == 3).Name);
        Assert.Equal(14, order.Items.First(i => i.Id == 3).Quantity);

        Assert.Throws<InvalidOperationException>(() => order.Items.First(i => i.Id == 4));
    }

    [Fact]
    public async Task Can_Func1CommandHandlerHandler_ThrowsException_WithNullItemName()
    {
        // Arrange
        DateTime orderDateTime = Randomizer<DateTime>.Create();
        int orderId = Randomizer<int>.Create();

#pragma warning disable CS8600
        string itemName = null;
#pragma warning restore CS8600

        Mock<IChildObjectsEfRepo> childObjectsEfRepoMock = new();
        Func1Command func1Command = new(orderId, orderDateTime, 2, itemName!, 18, 4);
        CancellationToken cancellationToken = new();

        IRequestHandler<Func1Command, bool> requestHandler =
            new Func1CommandHandler(childObjectsEfRepoMock.Object);

        Order order = new(orderDateTime);
        order.AddItem("item1", 10);
        order.AddItem("item2", 20);
        order.AddItem("item3", 14);
        order.AddItem("item4", 108);

        order.GetType().GetProperty("Id")!.SetValue(order, orderId);

        OrderItem item = order.Items.First(i => i.Name == "item1");
        item.GetType().GetProperty("Id")!.SetValue(item, 1);

        item = order.Items.First(i => i.Name == "item2");
        item.GetType().GetProperty("Id")!.SetValue(item, 2);

        item = order.Items.First(i => i.Name == "item3");
        item.GetType().GetProperty("Id")!.SetValue(item, 3);

        item = order.Items.First(i => i.Name == "item4");
        item.GetType().GetProperty("Id")!.SetValue(item, 4);

        childObjectsEfRepoMock
            .Setup(s => s.GetOrderAsync(orderId))
            .ReturnsAsync(order);

        // Act
        await Assert.ThrowsAsync<ArgumentNullException>(
            () => requestHandler.Handle(func1Command, cancellationToken));

        // Assert
        childObjectsEfRepoMock
            .Verify(v => v.GetOrderAsync(orderId),
                Times.Once);

        childObjectsEfRepoMock
            .Verify(v => v.SaveAllAsync(),
                Times.Never);

        childObjectsEfRepoMock.VerifyNoOtherCalls();

        Assert.Equal(orderId, order.Id);
        Assert.Equal(orderDateTime, order.OrderDate);

        Assert.Equal("item1", order.Items.First(i => i.Id == 1).Name);
        Assert.Equal(10, order.Items.First(i => i.Id == 1).Quantity);

        Assert.Equal("item2", order.Items.First(i => i.Id == 2).Name);
        Assert.Equal(20, order.Items.First(i => i.Id == 2).Quantity);

        Assert.Equal("item3", order.Items.First(i => i.Id == 3).Name);
        Assert.Equal(14, order.Items.First(i => i.Id == 3).Quantity);

        Assert.Equal("item4", order.Items.First(i => i.Id == 4).Name);
        Assert.Equal(108, order.Items.First(i => i.Id == 4).Quantity);
    }

    [Fact]
    public async Task Can_Func1CommandHandlerHandler_ThrowsException_WithZeroItemQuantity()
    {
        // Arrange
        DateTime orderDateTime = Randomizer<DateTime>.Create();
        int orderId = Randomizer<int>.Create();

        Mock<IChildObjectsEfRepo> childObjectsEfRepoMock = new();
        Func1Command func1Command = new(orderId, orderDateTime, 2, "updated name", 0, 4);
        CancellationToken cancellationToken = new();

        IRequestHandler<Func1Command, bool> requestHandler =
            new Func1CommandHandler(childObjectsEfRepoMock.Object);

        Order order = new(orderDateTime);
        order.AddItem("item1", 10);
        order.AddItem("item2", 20);
        order.AddItem("item3", 14);
        order.AddItem("item4", 108);

        order.GetType().GetProperty("Id")!.SetValue(order, orderId);

        OrderItem item = order.Items.First(i => i.Name == "item1");
        item.GetType().GetProperty("Id")!.SetValue(item, 1);

        item = order.Items.First(i => i.Name == "item2");
        item.GetType().GetProperty("Id")!.SetValue(item, 2);

        item = order.Items.First(i => i.Name == "item3");
        item.GetType().GetProperty("Id")!.SetValue(item, 3);

        item = order.Items.First(i => i.Name == "item4");
        item.GetType().GetProperty("Id")!.SetValue(item, 4);

        childObjectsEfRepoMock
            .Setup(s => s.GetOrderAsync(orderId))
            .ReturnsAsync(order);

        // Act
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(
            () => requestHandler.Handle(func1Command, cancellationToken));

        // Assert
        childObjectsEfRepoMock
            .Verify(v => v.GetOrderAsync(orderId),
                Times.Once);

        childObjectsEfRepoMock
            .Verify(v => v.SaveAllAsync(),
                Times.Never);

        childObjectsEfRepoMock.VerifyNoOtherCalls();

        Assert.Equal(orderId, order.Id);
        Assert.Equal(orderDateTime, order.OrderDate);

        Assert.Equal("item1", order.Items.First(i => i.Id == 1).Name);
        Assert.Equal(10, order.Items.First(i => i.Id == 1).Quantity);

        Assert.Equal("updated name", order.Items.First(i => i.Id == 2).Name);
        Assert.Equal(20, order.Items.First(i => i.Id == 2).Quantity);

        Assert.Equal("item3", order.Items.First(i => i.Id == 3).Name);
        Assert.Equal(14, order.Items.First(i => i.Id == 3).Quantity);

        Assert.Equal("item4", order.Items.First(i => i.Id == 4).Name);
        Assert.Equal(108, order.Items.First(i => i.Id == 4).Quantity);
    }

    [Fact]
    public async Task Can_Func1CommandHandlerHandler_ThrowsException_WithInvalidOrderId()
    {
        // Arrange
        DateTime orderDateTime = Randomizer<DateTime>.Create();
        int orderId = Randomizer<int>.Create();

        Mock<IChildObjectsEfRepo> childObjectsEfRepoMock = new();
        Func1Command func1Command = new(orderId, orderDateTime, 2, "updated name", 18, 4);
        CancellationToken cancellationToken = new();

        IRequestHandler<Func1Command, bool> requestHandler =
            new Func1CommandHandler(childObjectsEfRepoMock.Object);

        childObjectsEfRepoMock
            .Setup(s => s.GetOrderAsync(orderId))
            .Throws<InvalidOperationException>();

        // Act
        await Assert.ThrowsAsync<InvalidOperationException>(
            () => requestHandler.Handle(func1Command, cancellationToken));

        // Assert
        childObjectsEfRepoMock
            .Verify(v => v.GetOrderAsync(orderId),
                Times.Once);

        childObjectsEfRepoMock
            .Verify(v => v.SaveAllAsync(),
                Times.Never);

        childObjectsEfRepoMock.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task Can_Func1CommandHandlerHandler_ThrowsException_WithInvalidItemId()
    {
        // Arrange
        DateTime orderDateTime = Randomizer<DateTime>.Create();
        int orderId = Randomizer<int>.Create();

        Mock<IChildObjectsEfRepo> childObjectsEfRepoMock = new();
        Func1Command func1Command = new(orderId, orderDateTime, 105, "updated name", 18, 4);
        CancellationToken cancellationToken = new();

        IRequestHandler<Func1Command, bool> requestHandler =
            new Func1CommandHandler(childObjectsEfRepoMock.Object);

        Order order = new(orderDateTime);
        order.AddItem("item1", 10);
        order.AddItem("item2", 20);
        order.AddItem("item3", 14);
        order.AddItem("item4", 108);

        order.GetType().GetProperty("Id")!.SetValue(order, orderId);

        OrderItem item = order.Items.First(i => i.Name == "item1");
        item.GetType().GetProperty("Id")!.SetValue(item, 1);

        item = order.Items.First(i => i.Name == "item2");
        item.GetType().GetProperty("Id")!.SetValue(item, 2);

        item = order.Items.First(i => i.Name == "item3");
        item.GetType().GetProperty("Id")!.SetValue(item, 3);

        item = order.Items.First(i => i.Name == "item4");
        item.GetType().GetProperty("Id")!.SetValue(item, 4);

        childObjectsEfRepoMock
            .Setup(s => s.GetOrderAsync(orderId))
            .ReturnsAsync(order);

        // Act
        await Assert.ThrowsAsync<InvalidOperationException>(
            () => requestHandler.Handle(func1Command, cancellationToken));

        // Assert
        childObjectsEfRepoMock
            .Verify(v => v.GetOrderAsync(orderId),
                Times.Once);

        childObjectsEfRepoMock
            .Verify(v => v.SaveAllAsync(),
                Times.Never);

        childObjectsEfRepoMock.VerifyNoOtherCalls();

        Assert.Equal(orderId, order.Id);
        Assert.Equal(orderDateTime, order.OrderDate);

        Assert.Equal("item1", order.Items.First(i => i.Id == 1).Name);
        Assert.Equal(10, order.Items.First(i => i.Id == 1).Quantity);

        Assert.Equal("item2", order.Items.First(i => i.Id == 2).Name);
        Assert.Equal(20, order.Items.First(i => i.Id == 2).Quantity);

        Assert.Equal("item3", order.Items.First(i => i.Id == 3).Name);
        Assert.Equal(14, order.Items.First(i => i.Id == 3).Quantity);

        Assert.Equal("item4", order.Items.First(i => i.Id == 4).Name);
        Assert.Equal(108, order.Items.First(i => i.Id == 4).Quantity);
    }
}
