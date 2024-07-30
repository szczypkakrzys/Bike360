using MediatR;

namespace Bike360.Application.Features.DivingSchoolCustomers.Commands.DeleteDivingSchoolCustomer;

public class DeleteDivingSchoolCustomerCommand : IRequest<Unit>
{
    public int Id { get; set; }
}
