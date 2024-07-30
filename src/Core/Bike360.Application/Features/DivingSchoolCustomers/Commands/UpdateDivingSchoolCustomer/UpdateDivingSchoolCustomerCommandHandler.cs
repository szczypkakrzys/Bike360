using AutoMapper;
using Bike360.Application.Contracts.Persistence;
using Bike360.Application.Exceptions;
using Bike360.Domain.DivingSchool;
using MediatR;

namespace Bike360.Application.Features.DivingSchoolCustomers.Commands.UpdateDivingSchoolCustomer;

public class UpdateDivingSchoolCustomerCommandHandler : IRequestHandler<UpdateDivingSchoolCustomerCommand, Unit>
{
    private readonly IMapper _mapper;
    private readonly IDivingSchoolCustomerRepository _customerRepository;

    public UpdateDivingSchoolCustomerCommandHandler(
        IMapper mapper,
        IDivingSchoolCustomerRepository customerRepository)
    {
        _mapper = mapper;
        _customerRepository = customerRepository;
    }

    public async Task<Unit> Handle(
        UpdateDivingSchoolCustomerCommand request,
        CancellationToken cancellationToken)
    {
        var validator = new UpdateDivingSchoolCustomerCommandValidator(_customerRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (validationResult.Errors.Count != 0)
            throw new BadRequestException("Invalid customer data", validationResult);

        var customerToUpdate = _mapper.Map<DivingSchoolCustomer>(request);

        await _customerRepository.UpdateAsync(customerToUpdate);

        return Unit.Value;
    }
}
