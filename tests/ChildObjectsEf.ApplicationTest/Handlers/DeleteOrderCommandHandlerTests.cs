namespace ChildObjectsEf.ApplicationTest.Handlers;

public class DeleteOrderCommandHandlerTests
{
    [Fact]
    public async Task Can_DeleteOrderCommandHandler_DeleteProperOrder()
    {
        // Arrange        
        int orderId = Randomizer<int>.Create();
        DeleteOrderCommand deleteOrderCommand = new(orderId);
        CancellationToken cancellationToken = new();
        Mock<IChildObjectsEfRepo> childObjectsEfRepoMock = new();

        IRequestHandler<DeleteOrderCommand, bool> requestHandler =
            new DeleteOrderCommandHandler(childObjectsEfRepoMock.Object);

        DateTime orderDateTime = Randomizer<DateTime>.Create();
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

        childObjectsEfRepoMock
            .SetupGet(s => s.UnitOfWork)
            .Returns(new Mock<IUnitOfWork>().Object);

        // Act
        bool isSuccess = await requestHandler.Handle(deleteOrderCommand, cancellationToken);

        // Assert
        childObjectsEfRepoMock
            .Verify(v => v.GetOrderAsync(orderId),
                Times.Once);

        childObjectsEfRepoMock
            .Verify(v => v.DeleteOrder(It.Is<Order>(o => o.Id == orderId && o.OrderDate == orderDateTime)),
                Times.Once);

        childObjectsEfRepoMock
            .Verify(v => v.UnitOfWork.SaveChangesAsync(),
                Times.Once);

        childObjectsEfRepoMock.VerifyNoOtherCalls();

        Assert.True(isSuccess);
    }
}
