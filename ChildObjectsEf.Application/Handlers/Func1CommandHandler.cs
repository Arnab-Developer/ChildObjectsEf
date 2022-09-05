namespace ChildObjectsEf.Application.Commands;

public class Func1CommandHandler : IRequestHandler<Func1Command, bool>
{
    private readonly IChildObjectsEfRepo _childObjectsEfRepo;

    public Func1CommandHandler(IChildObjectsEfRepo childObjectsEfRepo)
    {
        _childObjectsEfRepo = childObjectsEfRepo;
    }

    async Task<bool> IRequestHandler<Func1Command, bool>.Handle(
        Func1Command request,
        CancellationToken cancellationToken)
    {
        Order order = await _childObjectsEfRepo.GetOrderAsync(request.OrderId);
        order.OrderDate = request.OrderDate;

        order.UpdateItemName(request.ItemIdToUpdate, request.ItemName);
        order.UpdateItemQuantity(request.ItemIdToUpdate, request.ItemQuantity);

        order.RemoveItem(request.ItemIdToDelete);

        await _childObjectsEfRepo.SaveAllAsync();
        return true;
    }
}
