namespace ChildObjectsEf.ApiTest.Commands;

public class CreateOrderCommandHandlerTests
{
    [Fact]
    public async Task Can_CreateOrderCommandHandler_CreateProperOrder()
    {
        // Arrange
        int orderId = Randomizer<int>.Create();
        DateTime orderDateTime = Randomizer<DateTime>.Create();

        Mock<IChildObjectsEfRepo> childObjectsEfRepoMock = new();
        CreateOrderCommand createOrderCommand = new(orderId, orderDateTime);
        CancellationToken cancellationToken = new();

        IRequestHandler<CreateOrderCommand, int> requestHandler =
            new CreateOrderCommandHandler(childObjectsEfRepoMock.Object);

        childObjectsEfRepoMock
            .Setup(s => s.CreateOrderAsync(It.Is<Order>(o => o.GetCurrentDate() == orderDateTime)))
            .ReturnsAsync(205);

        // Act
        int newOrderId = await requestHandler.Handle(createOrderCommand, cancellationToken);

        // Assert
        childObjectsEfRepoMock
            .Verify(v => v.CreateOrderAsync(It.Is<Order>(o => o.GetCurrentDate() == orderDateTime)),
                Times.Once);

        childObjectsEfRepoMock
            .Verify(v => v.SaveAllAsync(),
                Times.Once);

        Assert.Equal(205, newOrderId);
    }
}
