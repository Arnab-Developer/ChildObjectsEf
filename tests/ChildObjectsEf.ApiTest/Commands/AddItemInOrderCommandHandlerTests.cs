namespace ChildObjectsEf.ApiTest.Commands;

public class AddItemInOrderCommandHandlerTests
{
    [Fact]
    public async Task Can_AddItemInOrderCommandHandler_AddProperItemInOrder()
    {
        // Arrange
        DateTime orderDateTime = Randomizer<DateTime>.Create();
        int orderId = Randomizer<int>.Create();
        int itemId = Randomizer<int>.Create();
        string itemName = Randomizer<string>.Create();
        int itemQuantity = Randomizer<int>.Create();

        Mock<IChildObjectsEfRepo> childObjectsEfRepoMock = new();
        AddItemInOrderCommand addItemInOrderCommand = new(orderId, itemId, itemName, itemQuantity);
        CancellationToken cancellationToken = new();

        IRequestHandler<AddItemInOrderCommand, bool> requestHandler =
            new AddItemInOrderCommandHandler(childObjectsEfRepoMock.Object);

        Order order = new(orderId, orderDateTime);

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

        Assert.True(isSuccess);
        Assert.Contains(itemName, order.Items.Select(i => i.Name));
    }
}
