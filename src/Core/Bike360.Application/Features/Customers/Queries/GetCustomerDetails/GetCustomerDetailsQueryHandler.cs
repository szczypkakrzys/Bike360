using AutoMapper;
using Bike360.Application.Contracts.Persistence;
using Bike360.Application.Exceptions;
using Bike360.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Bike360.Application.Features.Customers.Queries.GetCustomerDetails;

public class GetCustomerDetailsQueryHandler : IRequestHandler<GetCustomerDetailsQuery, CustomerDetailsDto>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetCustomerDetailsQueryHandler> _logger;

    public GetCustomerDetailsQueryHandler(
        ICustomerRepository customerRepository,
        IMapper mapper,
        ILogger<GetCustomerDetailsQueryHandler> logger)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<CustomerDetailsDto> Handle(
        GetCustomerDetailsQuery request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting customer with ID = {CustomerId}", request.Id);

        var customerDetails = await _customerRepository.GetByIdAsync(request.Id) ??
                    throw new NotFoundException(nameof(Customer), request.Id);

        var data = _mapper.Map<CustomerDetailsDto>(customerDetails);

        return data;
    }
}
