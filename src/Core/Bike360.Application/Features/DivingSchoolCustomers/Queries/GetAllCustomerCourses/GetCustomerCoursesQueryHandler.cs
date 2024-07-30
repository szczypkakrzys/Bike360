using AutoMapper;
using Bike360.Application.Contracts.Persistence;
using Bike360.Application.Exceptions;
using MediatR;

namespace Bike360.Application.Features.DivingSchoolCustomers.Queries.GetAllCustomerCourses;

public class GetCustomerCoursesQueryHandler : IRequestHandler<GetCustomerCoursesQuery, IEnumerable<CustomerCourseDto>>
{
    private readonly IDivingSchoolCustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public GetCustomerCoursesQueryHandler(
        IDivingSchoolCustomerRepository customerRepository,
        IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CustomerCourseDto>> Handle(
        GetCustomerCoursesQuery request,
        CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdWithTours(request.Id) ??
                    throw new NotFoundException("Customer", request.Id);

        var result = _mapper.Map<IEnumerable<CustomerCourseDto>>(customer.DivingCourses);

        return result;
    }
}
