using AutoMapper;
using Bike360.Application.Contracts.Persistence;
using Bike360.Application.Exceptions;
using Bike360.Domain.DivingSchool;
using MediatR;

namespace Bike360.Application.Features.DivingSchoolCustomers.Queries.GetDivingSchoolCustomerDetails;

public class GetDivingSchoolCustomerDetailsQueryHandler : IRequestHandler<GetDivingSchoolCustomerDetailsQuery, DivingSchoolCustomerDetailsDto>
{
    private readonly IDivingSchoolCustomerRepository _customerRepository;
    private readonly IMapper _mapper;
    public GetDivingSchoolCustomerDetailsQueryHandler(
        IDivingSchoolCustomerRepository customerRepository,
        IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<DivingSchoolCustomerDetailsDto> Handle(
        GetDivingSchoolCustomerDetailsQuery request,
        CancellationToken cancellationToken)
    {
        var customerDetails = await _customerRepository.GetByIdAsync(request.Id) ??
                    throw new NotFoundException(nameof(DivingSchoolCustomer), request.Id);

        var data = _mapper.Map<DivingSchoolCustomerDetailsDto>(customerDetails);

        return data;
    }
}
