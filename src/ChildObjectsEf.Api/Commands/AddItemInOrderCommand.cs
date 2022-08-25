namespace ChildObjectsEf.Api.Commands;

internal class AddItemInOrderCommand : IRequest<bool>
{
    public int OrderId { get; set; }

    public string ItemName { get; set; }

    public int ItemQuantity { get; set; }

    public AddItemInOrderCommand(int orderId, string itemName, int itemQuantity)
    {
        OrderId = orderId;
        ItemName = itemName;
        ItemQuantity = itemQuantity;
    }
}
