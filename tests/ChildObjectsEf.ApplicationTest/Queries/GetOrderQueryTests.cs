using ChildObjectsEf.Application.Queries;
using DTOs = ChildObjectsEf.Domain.DTOs;

namespace ChildObjectsEf.ApplicationTest.Queries;

public class GetOrderQueryTests
{
    [Fact]
    public async Task Can_GetOrderQuery_WorkProperly()
    {
        // Arrange
        Mock<DTOs::IOrderQuery> queryMock = new();
        IGetOrderQuery getOrderQuery = new GetOrderQuery(queryMock.Object);
        int orderId = Randomizer<int>.Create();
        DateTime orderDate = Randomizer<DateTime>.Create();

        DTOs::Order order = new()
        {
            Id = orderId,
            OrderDate = orderDate,
            Items = new List<DTOs::OrderItem>()
            {
                new DTOs::OrderItem() { Id = 1, Name = "item 1", Quantity = 100 },
                new DTOs::OrderItem() { Id = 2, Name = "item 2", Quantity = 254 }
            }
        };

        queryMock
            .Setup(s => s.GetOrderAsync(orderId))
            .ReturnsAsync(order);

        // Act
        await getOrderQuery.GetOrderAsync(orderId);

        // Assert
        queryMock
            .Verify(v => v.GetOrderAsync(It.Is<int>(oi => oi == orderId)),
                Times.Once);

        Assert.Equal(orderId, order.Id);
        Assert.Equal(orderDate, order.OrderDate);

        Assert.Equal(1, order.Items.ElementAt(0).Id);
        Assert.Equal("item 1", order.Items.ElementAt(0).Name);
        Assert.Equal(100, order.Items.ElementAt(0).Quantity);

        Assert.Equal(2, order.Items.ElementAt(1).Id);
        Assert.Equal("item 2", order.Items.ElementAt(1).Name);
        Assert.Equal(254, order.Items.ElementAt(1).Quantity);
    }
}
