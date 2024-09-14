using AutoMapper;
using Bike360.Application.Contracts.Persistence;
using Bike360.Application.Exceptions;
using Bike360.Domain;
using MediatR;

namespace Bike360.Application.Features.Customers.Queries.GetCustomerDetails;

public class GetCustomerDetailsQueryHandler : IRequestHandler<GetCustomerDetailsQuery, CustomerDetailsDto>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;
    public GetCustomerDetailsQueryHandler(
        ICustomerRepository customerRepository,
        IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<CustomerDetailsDto> Handle(
        GetCustomerDetailsQuery request,
        CancellationToken cancellationToken)
    {
        var customerDetails = await _customerRepository.GetByIdWithAddressAsync(request.Id) ??
                    throw new NotFoundException(nameof(Customer), request.Id);

        var data = _mapper.Map<CustomerDetailsDto>(customerDetails);

        return data;
    }
}
