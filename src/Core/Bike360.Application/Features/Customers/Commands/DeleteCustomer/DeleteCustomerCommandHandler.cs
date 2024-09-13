using Bike360.Application.Contracts.Persistence;
using Bike360.Application.Exceptions;
using Bike360.Domain;
using MediatR;

namespace Bike360.Application.Features.Customers.Commands.DeleteCustomer;

public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, Unit>
{
    private readonly ICustomerRepository _customerRepository;

    public DeleteCustomerCommandHandler(ICustomerRepository customer) =>
        _customerRepository = customer;

    public async Task<Unit> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var customerToDelete = await _customerRepository.GetByIdAsync(request.Id) ??
                    throw new NotFoundException(nameof(Customer), request.Id);

        await _customerRepository.DeleteAsync(customerToDelete);

        return Unit.Value;
    }
}
