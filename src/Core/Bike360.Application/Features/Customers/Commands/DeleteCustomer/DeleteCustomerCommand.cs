using MediatR;

namespace Bike360.Application.Features.Customers.Commands.DeleteCustomer;

public class DeleteCustomerCommand : IRequest<Unit>
{
    public int Id { get; set; }
}
