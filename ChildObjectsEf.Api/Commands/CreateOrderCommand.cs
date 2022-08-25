using MediatR;

namespace ChildObjectsEf.Api.Commands;

internal class CreateOrderCommand : IRequest<int>
{
	public DateTime OrderDateTime { get; set; }

	public CreateOrderCommand(DateTime orderDateTime)
	{
		OrderDateTime = orderDateTime;
	}
}