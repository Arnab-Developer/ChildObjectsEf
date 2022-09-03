namespace ChildObjectsEf.ApiTest.Commands;

public class AddItemInOrderCommandHandlerTests
{
    [Fact]
    public async Task Can_AddItemInOrderCommandHandler_AddProperItemInOrder()
    {
        // Arrange        
        string itemName = Randomizer<string>.Create();
        int itemQuantity = Randomizer<int>.Create();
        int orderId = Randomizer<int>.Create();

        AddItemInOrderCommand addItemInOrderCommand = new(orderId, itemName, itemQuantity);
        CancellationToken cancellationToken = new();
        Mock<IChildObjectsEfRepo> childObjectsEfRepoMock = new();

        IRequestHandler<AddItemInOrderCommand, bool> requestHandler =
            new AddItemInOrderCommandHandler(childObjectsEfRepoMock.Object);

        DateTime orderDateTime = Randomizer<DateTime>.Create();
        Order order = new(orderDateTime);
        order.GetType().GetProperty("Id")!.SetValue(order, orderId);

        childObjectsEfRepoMock
            .Setup(s => s.GetOrderAsync(orderId))
            .ReturnsAsync(order);

        // Act
        bool isSuccess = await requestHandler.Handle(addItemInOrderCommand, cancellationToken);

        // Assert
        childObjectsEfRepoMock
            .Verify(v => v.GetOrderAsync(orderId),
                Times.Once);

        childObjectsEfRepoMock
            .Verify(v => v.SaveAllAsync(),
                Times.Once);

        childObjectsEfRepoMock.VerifyNoOtherCalls();

        Assert.True(isSuccess);
        Assert.Contains(itemName, order.Items.Select(i => i.Name));
    }

    [Fact]
    public async Task Can_AddItemInOrderCommandHandler_ThrowException_WithNullItemName()
    {
        // Arrange        
#pragma warning disable CS8600
        string itemName = null;
#pragma warning restore CS8600

        int itemQuantity = Randomizer<int>.Create();
        int orderId = Randomizer<int>.Create();

        AddItemInOrderCommand addItemInOrderCommand = new(orderId, itemName!, itemQuantity);
        CancellationToken cancellationToken = new();
        Mock<IChildObjectsEfRepo> childObjectsEfRepoMock = new();

        IRequestHandler<AddItemInOrderCommand, bool> requestHandler =
            new AddItemInOrderCommandHandler(childObjectsEfRepoMock.Object);

        DateTime orderDateTime = Randomizer<DateTime>.Create();
        Order order = new(orderDateTime);
        order.GetType().GetProperty("Id")!.SetValue(order, orderId);

        childObjectsEfRepoMock
            .Setup(s => s.GetOrderAsync(orderId))
            .ReturnsAsync(order);

        // Act
        await Assert.ThrowsAsync<ArgumentNullException>(
            () => requestHandler.Handle(addItemInOrderCommand, cancellationToken));

        // Assert
        childObjectsEfRepoMock
            .Verify(v => v.GetOrderAsync(orderId),
                Times.Once);

        childObjectsEfRepoMock
            .Verify(v => v.SaveAllAsync(),
                Times.Never);

        childObjectsEfRepoMock.VerifyNoOtherCalls();

        Assert.DoesNotContain(itemName, order.Items.Select(i => i.Name));
    }

    [Fact]
    public async Task Can_AddItemInOrderCommandHandler_ThrowException_WithZeroItemQuantity()
    {
        // Arrange
        string itemName = Randomizer<string>.Create();
        int itemQuantity = 0;
        int orderId = Randomizer<int>.Create();

        AddItemInOrderCommand addItemInOrderCommand = new(orderId, itemName, itemQuantity);
        CancellationToken cancellationToken = new();
        Mock<IChildObjectsEfRepo> childObjectsEfRepoMock = new();

        IRequestHandler<AddItemInOrderCommand, bool> requestHandler =
            new AddItemInOrderCommandHandler(childObjectsEfRepoMock.Object);

        DateTime orderDateTime = Randomizer<DateTime>.Create();
        Order order = new(orderDateTime);
        order.GetType().GetProperty("Id")!.SetValue(order, orderId);

        childObjectsEfRepoMock
            .Setup(s => s.GetOrderAsync(orderId))
            .ReturnsAsync(order);

        // Act
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(
            () => requestHandler.Handle(addItemInOrderCommand, cancellationToken));

        // Assert
        childObjectsEfRepoMock
            .Verify(v => v.GetOrderAsync(orderId),
                Times.Once);

        childObjectsEfRepoMock
            .Verify(v => v.SaveAllAsync(),
                Times.Never);

        childObjectsEfRepoMock.VerifyNoOtherCalls();

        Assert.DoesNotContain(itemName, order.Items.Select(i => i.Name));
    }

    [Fact]
    public async Task Can_AddItemInOrderCommandHandler_ThrowException_WithInvalidOrderId()
    {
        // Arrange
        string itemName = Randomizer<string>.Create();
        int itemQuantity = 0;
        int notExistOrderId = 2;

        AddItemInOrderCommand addItemInOrderCommand = new(notExistOrderId, itemName, itemQuantity);
        CancellationToken cancellationToken = new();
        Mock<IChildObjectsEfRepo> childObjectsEfRepoMock = new();

        IRequestHandler<AddItemInOrderCommand, bool> requestHandler =
            new AddItemInOrderCommandHandler(childObjectsEfRepoMock.Object);

        childObjectsEfRepoMock
            .Setup(s => s.GetOrderAsync(notExistOrderId))
            .Throws<InvalidOperationException>();

        // Act
        await Assert.ThrowsAsync<InvalidOperationException>(
            () => requestHandler.Handle(addItemInOrderCommand, cancellationToken));

        // Assert
        childObjectsEfRepoMock
            .Verify(v => v.GetOrderAsync(notExistOrderId),
                Times.Once);

        childObjectsEfRepoMock
            .Verify(v => v.SaveAllAsync(),
                Times.Never);

        childObjectsEfRepoMock.VerifyNoOtherCalls();
    }
}
