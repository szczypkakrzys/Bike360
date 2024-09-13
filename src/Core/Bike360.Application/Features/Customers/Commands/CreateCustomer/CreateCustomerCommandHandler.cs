using AutoMapper;
using Bike360.Application.Contracts.Persistence;
using Bike360.Application.Exceptions;
using Bike360.Domain;
using MediatR;

namespace Bike360.Application.Features.Customers.Commands.CreateCustomer;

public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, int>
{
    private readonly IMapper _mapper;
    private readonly ICustomerRepository _customerRepository;

    public CreateCustomerCommandHandler(
        IMapper mapper,
        ICustomerRepository customerRepository)
    {
        _mapper = mapper;
        _customerRepository = customerRepository;
    }

    public async Task<int> Handle(
        CreateCustomerCommand request,
        CancellationToken cancellationToken)
    {
        var validator = new CreateCustomerCommandValidator(_customerRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (validationResult.Errors.Count != 0)
            throw new BadRequestException("Invalid Customer", validationResult);

        var customerToCreate = _mapper.Map<Customer>(request);

        await _customerRepository.CreateAsync(customerToCreate);

        return customerToCreate.Id;
    }
}
