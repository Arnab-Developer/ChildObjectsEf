namespace ChildObjectsEf.ApiTest.Commands;

public class CreateOrderCommandHandlerTests
{
    [Fact]
    public async Task Can_CreateOrderCommandHandler_CreateProperOrder()
    {
        // Arrange
        Mock<IChildObjectsEfRepo> childObjectsEfRepoMock = new();
        DateTime orderDateTime = Randomizer<DateTime>.Create();
        CreateOrderCommand createOrderCommand = new(orderDateTime);
        CancellationToken cancellationToken = new();

        IRequestHandler<CreateOrderCommand, int> requestHandler = 
            new CreateOrderCommandHandler(childObjectsEfRepoMock.Object);

        childObjectsEfRepoMock
            .Setup(s => s.CreateOrderAsync(It.Is<Order>(o => o.OrderDate == orderDateTime)))
            .ReturnsAsync(205);

        // Act
        int newOrderId = await requestHandler.Handle(createOrderCommand, cancellationToken);

        // Assert
        childObjectsEfRepoMock
            .Verify(v => v.CreateOrderAsync(It.Is<Order>(o => o.OrderDate == orderDateTime)),
                Times.Once);

        childObjectsEfRepoMock
            .Verify(v => v.SaveAllAsync(),
                Times.Once);
        
        Assert.Equal(205, newOrderId);
    }
}
