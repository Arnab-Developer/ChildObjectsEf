using ChildObjectsEf.Domain.AggregatesModel.OrderAggregate;
using ChildObjectsEf.Domain.SeedData;

namespace ChildObjectsEf.ApiTest.Handlers;

public class UpdateItemInOrderCommandHandlerTests
{
    [Fact]
    public async Task Can_UpdateItemInOrderCommandHandler_UpdateItemInOrder()
    {
        // Arrange
        DateTime orderDateTime = Randomizer<DateTime>.Create();
        int orderId = Randomizer<int>.Create();
        int itemId = 2;
        string itemName = "updated item";
        int itemQuantity = 106;

        Mock<IChildObjectsEfRepo> childObjectsEfRepoMock = new();
        UpdateItemInOrderCommand updateItemInOrderCommand = new(orderId, itemId, itemName, itemQuantity);
        CancellationToken cancellationToken = new();

        IRequestHandler<UpdateItemInOrderCommand, bool> requestHandler =
            new UpdateItemInOrderCommandHandler(childObjectsEfRepoMock.Object);

        Order order = new(orderDateTime);
        order.AddItem("item1", 10);
        order.AddItem("item2", 20);

        order.GetType().GetProperty("Id")!.SetValue(order, orderId);

        OrderItem item = order.Items.First(i => i.Name == "item1");
        item.GetType().GetProperty("Id")!.SetValue(item, 1);

        item = order.Items.First(i => i.Name == "item2");
        item.GetType().GetProperty("Id")!.SetValue(item, 2);

        childObjectsEfRepoMock
            .Setup(s => s.GetOrderAsync(orderId))
            .ReturnsAsync(order);

        childObjectsEfRepoMock
            .SetupGet(s => s.UnitOfWork)
            .Returns(new Mock<IUnitOfWork>().Object);

        // Act
        bool isSuccess = await requestHandler.Handle(updateItemInOrderCommand, cancellationToken);

        // Assert
        childObjectsEfRepoMock
            .Verify(v => v.GetOrderAsync(orderId),
                Times.Once);

        childObjectsEfRepoMock
            .Verify(v => v.UnitOfWork.SaveChangesAsync(),
                Times.Once);

        childObjectsEfRepoMock.VerifyNoOtherCalls();

        Assert.True(isSuccess);

        Assert.Equal("item1", order.Items.First(i => i.Id == 1).Name);
        Assert.Equal(10, order.Items.First(i => i.Id == 1).Quantity);

        Assert.Equal("updated item", order.Items.First(i => i.Id == 2).Name);
        Assert.Equal(106, order.Items.First(i => i.Id == 2).Quantity);
    }

    [Fact]
    public async Task Can_UpdateItemInOrderCommandHandler_ThrowException_WithNullItemName()
    {
        // Arrange
        DateTime orderDateTime = Randomizer<DateTime>.Create();
        int orderId = Randomizer<int>.Create();
        int itemId = 2;

#pragma warning disable CS8600
        string itemName = null;
#pragma warning restore CS8600

        int itemQuantity = 106;

        Mock<IChildObjectsEfRepo> childObjectsEfRepoMock = new();
        UpdateItemInOrderCommand updateItemInOrderCommand = new(orderId, itemId, itemName!, itemQuantity);
        CancellationToken cancellationToken = new();

        IRequestHandler<UpdateItemInOrderCommand, bool> requestHandler =
            new UpdateItemInOrderCommandHandler(childObjectsEfRepoMock.Object);

        Order order = new(orderDateTime);
        order.AddItem("item1", 10);
        order.AddItem("item2", 20);

        order.GetType().GetProperty("Id")!.SetValue(order, orderId);

        OrderItem item = order.Items.First(i => i.Name == "item1");
        item.GetType().GetProperty("Id")!.SetValue(item, 1);

        item = order.Items.First(i => i.Name == "item2");
        item.GetType().GetProperty("Id")!.SetValue(item, 2);

        childObjectsEfRepoMock
            .Setup(s => s.GetOrderAsync(orderId))
            .ReturnsAsync(order);

        // Act
        await Assert.ThrowsAsync<ArgumentNullException>(
            () => requestHandler.Handle(updateItemInOrderCommand, cancellationToken));

        // Assert
        childObjectsEfRepoMock
            .Verify(v => v.GetOrderAsync(orderId),
                Times.Once);

        childObjectsEfRepoMock
            .Verify(v => v.UnitOfWork.SaveChangesAsync(),
                Times.Never);

        childObjectsEfRepoMock.VerifyNoOtherCalls();

        Assert.Equal("item1", order.Items.First(i => i.Id == 1).Name);
        Assert.Equal(10, order.Items.First(i => i.Id == 1).Quantity);

        Assert.Equal("item2", order.Items.First(i => i.Id == 2).Name);
        Assert.Equal(20, order.Items.First(i => i.Id == 2).Quantity);
    }

    [Fact]
    public async Task Can_UpdateItemInOrderCommandHandler_ThrowException_WithZeroItemQuantity()
    {
        // Arrange
        DateTime orderDateTime = Randomizer<DateTime>.Create();
        int orderId = Randomizer<int>.Create();
        int itemId = 2;
        string itemName = "updated item";
        int itemQuantity = 0;

        Mock<IChildObjectsEfRepo> childObjectsEfRepoMock = new();
        UpdateItemInOrderCommand updateItemInOrderCommand = new(orderId, itemId, itemName, itemQuantity);
        CancellationToken cancellationToken = new();

        IRequestHandler<UpdateItemInOrderCommand, bool> requestHandler =
            new UpdateItemInOrderCommandHandler(childObjectsEfRepoMock.Object);

        Order order = new(orderDateTime);
        order.AddItem("item1", 10);
        order.AddItem("item2", 20);

        order.GetType().GetProperty("Id")!.SetValue(order, orderId);

        OrderItem item = order.Items.First(i => i.Name == "item1");
        item.GetType().GetProperty("Id")!.SetValue(item, 1);

        item = order.Items.First(i => i.Name == "item2");
        item.GetType().GetProperty("Id")!.SetValue(item, 2);

        childObjectsEfRepoMock
            .Setup(s => s.GetOrderAsync(orderId))
            .ReturnsAsync(order);

        // Act
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(
            () => requestHandler.Handle(updateItemInOrderCommand, cancellationToken));

        // Assert
        childObjectsEfRepoMock
            .Verify(v => v.GetOrderAsync(orderId),
                Times.Once);

        childObjectsEfRepoMock
            .Verify(v => v.UnitOfWork.SaveChangesAsync(),
                Times.Never);

        childObjectsEfRepoMock.VerifyNoOtherCalls();

        Assert.Equal("item1", order.Items.First(i => i.Id == 1).Name);
        Assert.Equal(10, order.Items.First(i => i.Id == 1).Quantity);

        Assert.Equal("updated item", order.Items.First(i => i.Id == 2).Name);
        Assert.Equal(20, order.Items.First(i => i.Id == 2).Quantity);
    }

    [Fact]
    public async Task Can_UpdateItemInOrderCommandHandler_ThrowException_WithInvalidOrderId()
    {
        // Arrange
        DateTime orderDateTime = Randomizer<DateTime>.Create();
        int invalidOrderId = 2;
        int itemId = 2;
        string itemName = "updated item";
        int itemQuantity = 106;

        Mock<IChildObjectsEfRepo> childObjectsEfRepoMock = new();
        UpdateItemInOrderCommand updateItemInOrderCommand = new(invalidOrderId, itemId, itemName, itemQuantity);
        CancellationToken cancellationToken = new();

        IRequestHandler<UpdateItemInOrderCommand, bool> requestHandler =
            new UpdateItemInOrderCommandHandler(childObjectsEfRepoMock.Object);

        childObjectsEfRepoMock
            .Setup(s => s.GetOrderAsync(invalidOrderId))
            .Throws<InvalidOperationException>();

        childObjectsEfRepoMock
            .SetupGet(s => s.UnitOfWork)
            .Returns(new Mock<IUnitOfWork>().Object);

        // Act
        await Assert.ThrowsAsync<InvalidOperationException>(
            () => requestHandler.Handle(updateItemInOrderCommand, cancellationToken));

        // Assert
        childObjectsEfRepoMock
            .Verify(v => v.GetOrderAsync(invalidOrderId),
                Times.Once);

        childObjectsEfRepoMock
            .Verify(v => v.UnitOfWork.SaveChangesAsync(),
                Times.Never);

        childObjectsEfRepoMock.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task Can_UpdateItemInOrderCommandHandler_ThrowException_WithInvalidItemId()
    {
        // Arrange
        DateTime orderDateTime = Randomizer<DateTime>.Create();
        int orderId = Randomizer<int>.Create();
        int invalidItemId = 3;
        string itemName = "updated item";
        int itemQuantity = 106;

        Mock<IChildObjectsEfRepo> childObjectsEfRepoMock = new();
        UpdateItemInOrderCommand updateItemInOrderCommand = new(orderId, invalidItemId, itemName, itemQuantity);
        CancellationToken cancellationToken = new();

        IRequestHandler<UpdateItemInOrderCommand, bool> requestHandler =
            new UpdateItemInOrderCommandHandler(childObjectsEfRepoMock.Object);

        Order order = new(orderDateTime);
        order.AddItem("item1", 10);
        order.AddItem("item2", 20);

        order.GetType().GetProperty("Id")!.SetValue(order, orderId);

        OrderItem item = order.Items.First(i => i.Name == "item1");
        item.GetType().GetProperty("Id")!.SetValue(item, 1);

        item = order.Items.First(i => i.Name == "item2");
        item.GetType().GetProperty("Id")!.SetValue(item, 2);

        childObjectsEfRepoMock
            .Setup(s => s.GetOrderAsync(orderId))
            .ReturnsAsync(order);

        childObjectsEfRepoMock
            .SetupGet(s => s.UnitOfWork)
            .Returns(new Mock<IUnitOfWork>().Object);

        // Act
        await Assert.ThrowsAsync<InvalidOperationException>(
            () => requestHandler.Handle(updateItemInOrderCommand, cancellationToken));

        // Assert
        childObjectsEfRepoMock
            .Verify(v => v.GetOrderAsync(orderId),
                Times.Once);

        childObjectsEfRepoMock
            .Verify(v => v.UnitOfWork.SaveChangesAsync(),
                Times.Never);

        childObjectsEfRepoMock.VerifyNoOtherCalls();

        Assert.Equal("item1", order.Items.First(i => i.Id == 1).Name);
        Assert.Equal(10, order.Items.First(i => i.Id == 1).Quantity);

        Assert.Equal("item2", order.Items.First(i => i.Id == 2).Name);
        Assert.Equal(20, order.Items.First(i => i.Id == 2).Quantity);
    }
}
