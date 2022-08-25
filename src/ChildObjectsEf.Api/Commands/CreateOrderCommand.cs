using MediatR;

namespace ChildObjectsEf.Api.Commands;

internal class CreateOrderCommand : IRequest<int>
{
    public int Id { get; set; }

    public DateTime OrderDateTime { get; set; }

    public CreateOrderCommand(int id, DateTime orderDateTime)
    {
        Id = id;
        OrderDateTime = orderDateTime;
    }
}