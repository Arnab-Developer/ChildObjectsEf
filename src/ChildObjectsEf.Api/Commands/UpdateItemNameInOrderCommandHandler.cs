using ChildObjectsEf.Domain;
using MediatR;

namespace ChildObjectsEf.Api.Commands;

internal class UpdateItemInOrderCommandHandler : IRequestHandler<UpdateItemInOrderCommand, bool>
{
    private readonly IChildObjectsEfRepo _childObjectsEfRepo;

    public UpdateItemInOrderCommandHandler(IChildObjectsEfRepo childObjectsEfRepo)
    {
        _childObjectsEfRepo = childObjectsEfRepo;
    }

    async Task<bool> IRequestHandler<UpdateItemInOrderCommand, bool>.Handle(
        UpdateItemInOrderCommand request,
        CancellationToken cancellationToken)
    {
        Order order = await _childObjectsEfRepo.GetOrderAsync(request.OrderId);
        order.UpdateItemName(request.ItemId, request.ItemName);
        order.UpdateItemQuantity(request.ItemId, request.ItemQuantity);
        await _childObjectsEfRepo.SaveAllAsync();
        return true;
    }
}
