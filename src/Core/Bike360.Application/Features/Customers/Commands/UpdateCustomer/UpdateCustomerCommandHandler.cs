using AutoMapper;
using Bike360.Application.Contracts.Persistence;
using Bike360.Application.Exceptions;
using Bike360.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Bike360.Application.Features.Customers.Commands.UpdateCustomer;

public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, Unit>
{
    private readonly IMapper _mapper;
    private readonly ICustomerRepository _customerRepository;
    private readonly ILogger<UpdateCustomerCommandHandler> _logger;

    public UpdateCustomerCommandHandler(
        IMapper mapper,
        ICustomerRepository customerRepository,
        ILogger<UpdateCustomerCommandHandler> logger)
    {
        _mapper = mapper;
        _customerRepository = customerRepository;
        _logger = logger;
    }

    public async Task<Unit> Handle(
        UpdateCustomerCommand request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating customer with ID = {CustomerId} with {@UpdatedCustomer}", request.Id, request);

        var validator = new UpdateCustomerCommandValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (validationResult.Errors.Count != 0)
            throw new BadRequestException("Invalid customer data", validationResult);

        var customerData = await _customerRepository.GetByIdAsync(request.Id)
            ?? throw new NotFoundException(nameof(Customer), request.Id);

        _mapper.Map(request, customerData);

        await _customerRepository.UpdateAsync(customerData);

        return Unit.Value;
    }
}
