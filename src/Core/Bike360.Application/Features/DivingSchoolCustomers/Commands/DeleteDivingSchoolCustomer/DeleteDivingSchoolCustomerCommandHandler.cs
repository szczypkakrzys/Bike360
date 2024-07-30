using Bike360.Application.Contracts.Persistence;
using Bike360.Application.Exceptions;
using Bike360.Domain.DivingSchool;
using MediatR;

namespace Bike360.Application.Features.DivingSchoolCustomers.Commands.DeleteDivingSchoolCustomer;

public class DeleteDivingSchoolCustomerCommandHandler : IRequestHandler<DeleteDivingSchoolCustomerCommand, Unit>
{
    private readonly IDivingSchoolCustomerRepository _customerRepository;

    public DeleteDivingSchoolCustomerCommandHandler(IDivingSchoolCustomerRepository customer) =>
        _customerRepository = customer;

    public async Task<Unit> Handle(DeleteDivingSchoolCustomerCommand request, CancellationToken cancellationToken)
    {
        var customerToDelete = await _customerRepository.GetByIdAsync(request.Id) ??
                    throw new NotFoundException(nameof(DivingSchoolCustomer), request.Id);

        await _customerRepository.DeleteAsync(customerToDelete);

        return Unit.Value;
    }
}
