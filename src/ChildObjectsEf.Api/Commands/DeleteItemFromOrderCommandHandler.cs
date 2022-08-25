using ChildObjectsEf.Domain;
using MediatR;

namespace ChildObjectsEf.Api.Commands;

internal class DeleteItemFromOrderCommandHandler : IRequestHandler<DeleteItemFromOrderCommand, bool>
{
    private readonly IChildObjectsEfRepo _childObjectsEfRepo;

    public DeleteItemFromOrderCommandHandler(IChildObjectsEfRepo childObjectsEfRepo)
    {
        _childObjectsEfRepo = childObjectsEfRepo;
    }

    async Task<bool> IRequestHandler<DeleteItemFromOrderCommand, bool>.Handle(
        DeleteItemFromOrderCommand request,
        CancellationToken cancellationToken)
    {
        Order order = await _childObjectsEfRepo.GetOrderAsync(request.OrderId);
        order.RemoveItem(request.ItemId);
        await _childObjectsEfRepo.SaveAllAsync();
        return true;
    }
}
