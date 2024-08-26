using AutoMapper;
using Bike360.Application.Contracts.Persistence;
using MediatR;

namespace Bike360.Application.Features.DivingSchoolCustomers.Queries.GetAllDivingSchoolCustomers;

public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, IEnumerable<CustomerDto>>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public GetAllCustomersQueryHandler(
        IMapper mapper,
        ICustomerRepository customerRepository)
    {
        _mapper = mapper;
        _customerRepository = customerRepository;
    }

    public async Task<IEnumerable<CustomerDto>> Handle(
        GetAllCustomersQuery request,
        CancellationToken cancellationToken)
    {
        var customers = await _customerRepository.GetAsync();

        var data = _mapper.Map<IEnumerable<CustomerDto>>(customers);

        return data;
    }
}
