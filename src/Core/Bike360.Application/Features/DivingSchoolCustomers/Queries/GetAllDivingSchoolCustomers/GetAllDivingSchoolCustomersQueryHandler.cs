using AutoMapper;
using Bike360.Application.Contracts.Persistence;
using MediatR;

namespace Bike360.Application.Features.DivingSchoolCustomers.Queries.GetAllDivingSchoolCustomers;

public class GetAllDivingSchoolCustomersQueryHandler : IRequestHandler<GetDivingSchoolCustomersQuery, IEnumerable<DivingSchoolCustomerDto>>
{
    private readonly IDivingSchoolCustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public GetAllDivingSchoolCustomersQueryHandler(
        IMapper mapper,
        IDivingSchoolCustomerRepository customerRepository)
    {
        _mapper = mapper;
        _customerRepository = customerRepository;
    }

    public async Task<IEnumerable<DivingSchoolCustomerDto>> Handle(
        GetDivingSchoolCustomersQuery request,
        CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetAsync();

        var data = _mapper.Map<IEnumerable<DivingSchoolCustomerDto>>(customer);

        return data;
    }
}
