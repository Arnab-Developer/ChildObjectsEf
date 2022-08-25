namespace ChildObjectsEf.ApiTest.Commands;

public class DeleteItemFromOrderCommandHandlerTests
{
    [Fact]
    public async Task Can_DeleteItemFromOrderCommandHandler_UpdateItemInOrder()
    {
        // Arrange
        DateTime orderDateTime = Randomizer<DateTime>.Create();
        int orderId = Randomizer<int>.Create();

        Mock<IChildObjectsEfRepo> childObjectsEfRepoMock = new();
        DeleteItemFromOrderCommand deleteItemFromOrderCommand = new(orderId, 2);
        CancellationToken cancellationToken = new();

        IRequestHandler<DeleteItemFromOrderCommand, bool> requestHandler =
            new DeleteItemFromOrderCommandHandler(childObjectsEfRepoMock.Object);

        Order order = new(orderId, orderDateTime);
        order.AddItem(1, "item1", 10);
        order.AddItem(2, "item2", 20);

        childObjectsEfRepoMock
            .Setup(s => s.GetOrderAsync(orderId))
            .ReturnsAsync(order);

        // Act
        bool isSuccess = await requestHandler.Handle(deleteItemFromOrderCommand, cancellationToken);

        // Assert
        childObjectsEfRepoMock
            .Verify(v => v.GetOrderAsync(orderId),
                Times.Once);

        childObjectsEfRepoMock
            .Verify(v => v.SaveAllAsync(),
                Times.Once);

        Assert.True(isSuccess);
        Assert.Single(order.Items);

        Assert.Equal("item1", order.Items.First(i => i.Id == 1).Name);
        Assert.Equal(10, order.Items.First(i => i.Id == 1).Quantity);

        Assert.Throws<InvalidOperationException>(() => order.Items.First(i => i.Id == 2));
    }
}
