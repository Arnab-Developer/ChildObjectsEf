namespace ChildObjectsEf.Application.Handlers;

public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, bool>
{
    private readonly IChildObjectsEfRepo _childObjectsEfRepo;

    public DeleteOrderCommandHandler(IChildObjectsEfRepo childObjectsEfRepo)
    {
        _childObjectsEfRepo = childObjectsEfRepo;
    }

    async Task<bool> IRequestHandler<DeleteOrderCommand, bool>.Handle(
        DeleteOrderCommand request,
        CancellationToken cancellationToken)
    {
        Order order = await _childObjectsEfRepo.GetOrderAsync(request.OrderId);
        _childObjectsEfRepo.DeleteOrder(order);
        await _childObjectsEfRepo.UnitOfWork.SaveChangesAsync();
        return true;
    }
}
