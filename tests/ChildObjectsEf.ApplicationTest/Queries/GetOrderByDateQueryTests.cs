﻿namespace ChildObjectsEf.ApplicationTest.Queries;

public class GetOrderByDateQueryTests
{
    [Fact]
    public async Task Can_GetOrderByDateAsync_WorkProperly()
    {
        // Arrange
        Mock<DTOs::IOrderQuery> queryMock = new();
        IGetOrderByDateQuery getOrderByDateQuery = new GetOrderByDateQuery(queryMock.Object);
        int orderId = Randomizer<int>.Create();
        DateTime orderDate = Randomizer<DateTime>.Create();

        List<DTOs::Order> orders = new()
        {
            new DTOs::Order()
            {
                Id = orderId,
                OrderDate = orderDate,
                Items = new List<DTOs::OrderItem>()
                {
                    new DTOs::OrderItem() { Id = 1, Name = "item 1", Quantity = 100 },
                    new DTOs::OrderItem() { Id = 2, Name = "item 2", Quantity = 254 }
                }
            },
            new DTOs::Order()
            {
                Id = 2,
                OrderDate = DateTime.Now,
                Items = new List<DTOs::OrderItem>()
                {
                    new DTOs::OrderItem() { Id = 3, Name = "item 3", Quantity = 1478 },
                    new DTOs::OrderItem() { Id = 4, Name = "item 4", Quantity = 8574 }
                }
            }
        };

        queryMock
            .Setup(s => s.GetOrderByDateAsync(orderDate))
            .ReturnsAsync(orders);

        // Act
        await getOrderByDateQuery.GetOrderByDateAsync(orderDate);

        // Assert
        queryMock
            .Verify(v => v.GetOrderByDateAsync(It.Is<DateTime>(oi => oi == orderDate)),
                Times.Once);

        Assert.Equal(orderId, orders[0].Id);
        Assert.Equal(orderDate, orders[0].OrderDate);

        Assert.Equal(1, orders[0].Items.ElementAt(0).Id);
        Assert.Equal("item 1", orders[0].Items.ElementAt(0).Name);
        Assert.Equal(100, orders[0].Items.ElementAt(0).Quantity);

        Assert.Equal(2, orders[0].Items.ElementAt(1).Id);
        Assert.Equal("item 2", orders[0].Items.ElementAt(1).Name);
        Assert.Equal(254, orders[0].Items.ElementAt(1).Quantity);

        Assert.Equal(2, orders[1].Id);
        Assert.Equal(DateTime.Now.Date, orders[1].OrderDate.Date);

        Assert.Equal(3, orders[1].Items.ElementAt(0).Id);
        Assert.Equal("item 3", orders[1].Items.ElementAt(0).Name);
        Assert.Equal(1478, orders[1].Items.ElementAt(0).Quantity);

        Assert.Equal(4, orders[1].Items.ElementAt(1).Id);
        Assert.Equal("item 4", orders[1].Items.ElementAt(1).Name);
        Assert.Equal(8574, orders[1].Items.ElementAt(1).Quantity);
    }
}
