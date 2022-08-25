using MediatR;

namespace ChildObjectsEf.Api.Commands;

public class CreateOrderCommand : IRequest
{
	public DateTime OrderDateTime { get; set; }

	public CreateOrderCommand(DateTime orderDateTime)
	{
		OrderDateTime = orderDateTime;
	}
}