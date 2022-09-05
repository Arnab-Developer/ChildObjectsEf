namespace ChildObjectsEf.Application.Commands;

public class AddItemInOrderCommandHandler : IRequestHandler<AddItemInOrderCommand, bool>
{
    private readonly IChildObjectsEfRepo _childObjectsEfRepo;

    public AddItemInOrderCommandHandler(IChildObjectsEfRepo childObjectsEfRepo)
    {
        _childObjectsEfRepo = childObjectsEfRepo;
    }

    async Task<bool> IRequestHandler<AddItemInOrderCommand, bool>.Handle(
        AddItemInOrderCommand request,
        CancellationToken cancellationToken)
    {
        Order order = await _childObjectsEfRepo.GetOrderAsync(request.OrderId);
        order.AddItem(request.ItemName, request.ItemQuantity);
        await _childObjectsEfRepo.SaveAllAsync();
        return true;
    }
}
