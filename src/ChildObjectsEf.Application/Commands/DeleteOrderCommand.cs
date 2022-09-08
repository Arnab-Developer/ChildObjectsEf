namespace ChildObjectsEf.Application.Commands;

public class DeleteOrderCommand : IRequest<bool>
{
    public int OrderId { get; set; }

    public DeleteOrderCommand(int orderId)
    {
        OrderId = orderId;
    }
}
