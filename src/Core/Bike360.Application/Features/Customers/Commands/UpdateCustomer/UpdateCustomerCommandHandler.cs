﻿using AutoMapper;
using Bike360.Application.Contracts.Persistence;
using Bike360.Application.Exceptions;
using Bike360.Domain;
using MediatR;

namespace Bike360.Application.Features.Customers.Commands.UpdateCustomer;

public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, Unit>
{
    private readonly IMapper _mapper;
    private readonly ICustomerRepository _customerRepository;

    public UpdateCustomerCommandHandler(
        IMapper mapper,
        ICustomerRepository customerRepository)
    {
        _mapper = mapper;
        _customerRepository = customerRepository;
    }

    public async Task<Unit> Handle(
        UpdateCustomerCommand request,
        CancellationToken cancellationToken)
    {
        var validator = new UpdateCustomerCommandValidator(_customerRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (validationResult.Errors.Count != 0)
            throw new BadRequestException("Invalid customer data", validationResult);

        var customerToUpdate = _mapper.Map<Customer>(request);

        await _customerRepository.UpdateAsync(customerToUpdate);

        return Unit.Value;
    }
}
