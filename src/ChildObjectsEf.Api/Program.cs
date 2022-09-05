using ChildObjectsEf.Application.Commands;
using ChildObjectsEf.Data;
using ChildObjectsEf.Domain.AggregatesModel.OrderAggregate;
using MediatR;
using DTOs = ChildObjectsEf.Domain.DTOs;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(typeof(CreateOrderCommand));
builder.Services.AddTransient<IChildObjectsEfRepo, ChildObjectsEfRepo>();
builder.Services.AddSqlServer<ChildObjectsEfContext>(builder.Configuration.GetConnectionString("ChildObjectsEfConnection"));
builder.Services.AddTransient<DTOs::IOrderQuery>(options =>
{
    string constr = builder.Configuration.GetConnectionString("ChildObjectsEfConnection");
    OrderQuery orderQuery = new(constr);
    return orderQuery;
});

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/get-order", async (
    DTOs::IOrderQuery orderQuery,
    int orderId) =>
{
    DTOs::Order order = await orderQuery.GetOrderAsync(orderId);
    return order;
});

app.MapGet("/create-order", async (
    IMediator mediator,
    string dateTime) =>
{
    DateTime orderDateTime = DateTime.Parse(dateTime);
    CreateOrderCommand command = new(orderDateTime);
    await mediator.Send(command);
});

app.MapGet("/add-item-in-order", async (
    IMediator mediator,
    int orderId,
    string itemName,
    int itemQuantity) =>
{
    AddItemInOrderCommand command = new(orderId, itemName, itemQuantity);
    await mediator.Send(command);
});

app.MapGet("/update-item-in-order", async (
    IMediator mediator,
    int orderId,
    int itemId,
    string itemName,
    int itemQuantity) =>
{
    UpdateItemInOrderCommand command = new(orderId, itemId, itemName, itemQuantity);
    await mediator.Send(command);
});

app.MapGet("/delete-item-in-order", async (
    IMediator mediator,
    int orderId,
    int itemId) =>
{
    DeleteItemFromOrderCommand command = new(orderId, itemId);
    await mediator.Send(command);
});

app.MapGet("/func1", async (
    IMediator mediator,
    int orderId,
    string dateTime,
    int itemIdToUpdate,
    string itemName,
    int itemQuantity,
    int itemIdToDelete) =>
{
    DateTime orderDateTime = DateTime.Parse(dateTime);

    Func1Command command = new(
        orderId,
        orderDateTime,
        itemIdToUpdate,
        itemName,
        itemQuantity,
        itemIdToDelete);

    await mediator.Send(command);
});

app.Run();