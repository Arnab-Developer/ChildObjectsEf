using ChildObjectsEf.Domain.AggregatesModel.OrderAggregate;
using ChildObjectsEf.Domain.SeedData;

namespace ChildObjectsEf.ApiTest.Handlers;

public class DeleteItemFromOrderCommandHandlerTests
{
    [Fact]
    public async Task Can_DeleteItemFromOrderCommandHandler_DeleteItemInOrder()
    {
        // Arrange
        DateTime orderDateTime = Randomizer<DateTime>.Create();
        int orderId = Randomizer<int>.Create();

        Mock<IChildObjectsEfRepo> childObjectsEfRepoMock = new();
        DeleteItemFromOrderCommand deleteItemFromOrderCommand = new(orderId, 2);
        CancellationToken cancellationToken = new();

        IRequestHandler<DeleteItemFromOrderCommand, bool> requestHandler =
            new DeleteItemFromOrderCommandHandler(childObjectsEfRepoMock.Object);

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
        bool isSuccess = await requestHandler.Handle(deleteItemFromOrderCommand, cancellationToken);

        // Assert
        childObjectsEfRepoMock
            .Verify(v => v.GetOrderAsync(orderId),
                Times.Once);

        childObjectsEfRepoMock
            .Verify(v => v.UnitOfWork.SaveChangesAsync(),
                Times.Once);

        childObjectsEfRepoMock.VerifyNoOtherCalls();

        Assert.True(isSuccess);
        Assert.Single(order.Items);

        Assert.Equal("item1", order.Items.First(i => i.Id == 1).Name);
        Assert.Equal(10, order.Items.First(i => i.Id == 1).Quantity);

        Assert.Throws<InvalidOperationException>(() => order.Items.First(i => i.Id == 2));
    }

    [Fact]
    public async Task Can_DeleteItemFromOrderCommandHandler_ThrowsException_WithInvalidOrderId()
    {
        // Arrange
        DateTime orderDateTime = Randomizer<DateTime>.Create();
        Mock<IChildObjectsEfRepo> childObjectsEfRepoMock = new();
        int orderId = 1;
        DeleteItemFromOrderCommand deleteItemFromOrderCommand = new(orderId, 2);
        CancellationToken cancellationToken = new();

        IRequestHandler<DeleteItemFromOrderCommand, bool> requestHandler =
            new DeleteItemFromOrderCommandHandler(childObjectsEfRepoMock.Object);

        childObjectsEfRepoMock
            .Setup(s => s.GetOrderAsync(orderId))
            .Throws<InvalidOperationException>();

        childObjectsEfRepoMock
            .SetupGet(s => s.UnitOfWork)
            .Returns(new Mock<IUnitOfWork>().Object);

        // Act
        await Assert.ThrowsAsync<InvalidOperationException>(
            () => requestHandler.Handle(deleteItemFromOrderCommand, cancellationToken));

        // Assert
        childObjectsEfRepoMock
            .Verify(v => v.GetOrderAsync(orderId),
                Times.Once);

        childObjectsEfRepoMock
            .Verify(v => v.UnitOfWork.SaveChangesAsync(),
                Times.Never);

        childObjectsEfRepoMock.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task Can_DeleteItemFromOrderCommandHandler_ThrowsException_WithInvalidItemId()
    {
        // Arrange
        DateTime orderDateTime = Randomizer<DateTime>.Create();
        int orderId = Randomizer<int>.Create();

        Mock<IChildObjectsEfRepo> childObjectsEfRepoMock = new();
        DeleteItemFromOrderCommand deleteItemFromOrderCommand = new(orderId, 3);
        CancellationToken cancellationToken = new();

        IRequestHandler<DeleteItemFromOrderCommand, bool> requestHandler =
            new DeleteItemFromOrderCommandHandler(childObjectsEfRepoMock.Object);

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
            () => requestHandler.Handle(deleteItemFromOrderCommand, cancellationToken));

        // Assert
        childObjectsEfRepoMock
            .Verify(v => v.GetOrderAsync(orderId),
                Times.Once);

        childObjectsEfRepoMock
            .Verify(v => v.UnitOfWork.SaveChangesAsync(),
                Times.Never);

        childObjectsEfRepoMock.VerifyNoOtherCalls();

        Assert.Equal(2, order.Items.Count());

        Assert.Equal("item1", order.Items.First(i => i.Id == 1).Name);
        Assert.Equal(10, order.Items.First(i => i.Id == 1).Quantity);

        Assert.Equal("item2", order.Items.First(i => i.Id == 2).Name);
        Assert.Equal(20, order.Items.First(i => i.Id == 2).Quantity);
    }
}
