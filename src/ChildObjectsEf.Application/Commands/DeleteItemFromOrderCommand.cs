namespace ChildObjectsEf.Application.Commands;

public class DeleteItemFromOrderCommand : IRequest<bool>
{
    public int OrderId { get; set; }

    public int ItemId { get; set; }

    public DeleteItemFromOrderCommand(int orderId, int itemId)
    {
        OrderId = orderId;
        ItemId = itemId;
    }
}
