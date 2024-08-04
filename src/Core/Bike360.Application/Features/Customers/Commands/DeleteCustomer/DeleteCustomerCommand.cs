using MediatR;

namespace Bike360.Application.Features.DivingSchoolCustomers.Commands.DeleteDivingSchoolCustomer;

public class DeleteCustomerCommand : IRequest<Unit>
{
    public int Id { get; set; }
}
