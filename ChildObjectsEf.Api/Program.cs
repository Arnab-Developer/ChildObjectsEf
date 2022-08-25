using ChildObjectsEf.Api.Commands;
using ChildObjectsEf.Data;
using ChildObjectsEf.Domain;
using MediatR;

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

app.MapGet("/create-order", async (IMediator mediator, DateTime orderDateTime) =>
{
    CreateOrderCommand createOrderCommand = new(orderDateTime);
    await mediator.Send(createOrderCommand);
})
.WithName("CreateOrder");

app.Run();