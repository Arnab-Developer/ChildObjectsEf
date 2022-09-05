namespace ChildObjectsEf.Application.Commands;

public class CreateOrderCommand : IRequest<int>
{
    public DateTime OrderDateTime { get; set; }

    public CreateOrderCommand(DateTime orderDateTime)
    {
        OrderDateTime = orderDateTime;
    }
}