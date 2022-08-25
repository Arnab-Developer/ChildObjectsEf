using ChildObjectsEf.Domain;
using MediatR;

namespace ChildObjectsEf.Api.Commands;

internal class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, int>
{
    private readonly IChildObjectsEfRepo _childObjectsEfRepo;

    public CreateOrderCommandHandler(IChildObjectsEfRepo childObjectsEfRepo)
    {
        _childObjectsEfRepo = childObjectsEfRepo;
    }

    async Task<int> IRequestHandler<CreateOrderCommand, int>.Handle(
        CreateOrderCommand request, 
        CancellationToken cancellationToken)
    {
        Order order = new(request.OrderDateTime);
        int newOrderId = await _childObjectsEfRepo.CreateOrderAsync(order);
        await _childObjectsEfRepo.SaveAllAsync();
        return newOrderId;
    }
}
