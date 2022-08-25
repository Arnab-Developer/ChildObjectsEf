using ChildObjectsEf.Api.Commands;
using ChildObjectsEf.Data;
using ChildObjectsEf.Domain;
using MediatR;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("ChildObjectsEf.ApiTest")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(typeof(Program));
builder.Services.AddTransient<IChildObjectsEfRepo, ChildObjectsEfRepo>();
builder.Services.AddSqlServer<ChildObjectsEfContext>(builder.Configuration.GetConnectionString("ChildObjectsEfConnection"));

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

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

app.Run();