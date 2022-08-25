using MediatR;

namespace ChildObjectsEf.Api.Commands;

internal class AddItemInOrderCommand : IRequest<bool>
{
    public int OrderId { get; set; }

    public int OrderItemId { get; set; }

    public string ItemName { get; set; }

    public int ItemQuantity { get; set; }

    public AddItemInOrderCommand(int orderId, int orderItemId, string itemName, int itemQuantity)
    {
        OrderId = orderId;
        OrderItemId = orderItemId;
        ItemName = itemName;
        ItemQuantity = itemQuantity;
    }
}
