namespace ChildObjectsEf.Api.Commands;

internal class UpdateItemInOrderCommand : IRequest<bool>
{
    public int OrderId { get; set; }

    public int ItemId { get; set; }

    public string ItemName { get; set; }

    public int ItemQuantity { get; set; }

    public UpdateItemInOrderCommand(int orderId, int itemId, string itemName, int itemQuantity)
    {
        OrderId = orderId;
        ItemId = itemId;
        ItemName = itemName;
        ItemQuantity = itemQuantity;
    }
}
