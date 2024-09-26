using AutoMapper;
using Bike360.Application.Contracts.Persistence;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Bike360.Application.Features.Customers.Queries.GetAllCustomers;

public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, IEnumerable<CustomerDto>>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly ILogger<GetAllCustomersQueryHandler> _logger;
    private readonly IMapper _mapper;

    public GetAllCustomersQueryHandler(
        IMapper mapper,
        ICustomerRepository customerRepository,
        ILogger<GetAllCustomersQueryHandler> logger)
    {
        _mapper = mapper;
        _customerRepository = customerRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<CustomerDto>> Handle(
        GetAllCustomersQuery request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all customers");

        var customers = await _customerRepository.GetAsync();

        var data = _mapper.Map<IEnumerable<CustomerDto>>(customers);

        return data;
    }
}
