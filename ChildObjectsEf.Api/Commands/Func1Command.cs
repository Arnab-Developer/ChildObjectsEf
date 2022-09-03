namespace ChildObjectsEf.Api.Commands;

internal class Func1Command : IRequest<bool>
{
    public int OrderId { get; set; }

    public DateTime OrderDate { get; set; }

    public int ItemIdToUpdate { get; set; }

    public string ItemName { get; set; }

    public int ItemQuantity { get; set; }

    public int ItemIdToDelete { get; set; }

    public Func1Command(
        int orderId,
        DateTime orderDate,
        int itemIdToUpdate,
        string itemName,
        int itemQuantity,
        int itemIdToDelete)
    {
        OrderId = orderId;
        OrderDate = orderDate;
        ItemIdToUpdate = itemIdToUpdate;
        ItemName = itemName;
        ItemQuantity = itemQuantity;
        ItemIdToDelete = itemIdToDelete;
    }
}
