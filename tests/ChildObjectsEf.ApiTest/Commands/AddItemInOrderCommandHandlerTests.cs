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

        Assert.True(isSuccess);
        Assert.Contains(itemName, order.Items.Select(i => i.Name));
    }
}
