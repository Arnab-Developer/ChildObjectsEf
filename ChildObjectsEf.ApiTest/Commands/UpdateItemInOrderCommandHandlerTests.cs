namespace ChildObjectsEf.ApiTest.Commands;

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

        Order order = new(orderId, orderDateTime);
        order.AddItem(1, "item1", 10);
        order.AddItem(2, "item2", 20);

        childObjectsEfRepoMock
            .Setup(s => s.GetOrderAsync(orderId))
            .ReturnsAsync(order);

        // Act
        bool isSuccess = await requestHandler.Handle(updateItemInOrderCommand, cancellationToken);

        // Assert
        childObjectsEfRepoMock
            .Verify(v => v.GetOrderAsync(orderId),
                Times.Once);

        childObjectsEfRepoMock
            .Verify(v => v.SaveAllAsync(),
                Times.Once);

        Assert.True(isSuccess);

        Assert.Equal("item1", order.Items.First(i => i.GetCurrentId() == 1).GetCurrentName());
        Assert.Equal(10, order.Items.First(i => i.GetCurrentId() == 1).GetCurrentQuantity());

        Assert.Equal("updated item", order.Items.First(i => i.GetCurrentId() == 2).GetCurrentName());
        Assert.Equal(106, order.Items.First(i => i.GetCurrentId() == 2).GetCurrentQuantity());        
    }
}
