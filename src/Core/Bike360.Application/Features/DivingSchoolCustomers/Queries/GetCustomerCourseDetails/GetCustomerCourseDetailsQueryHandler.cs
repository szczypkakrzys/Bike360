using AutoMapper;
using Bike360.Application.Contracts.Persistence;
using Bike360.Application.Exceptions;
using MediatR;

namespace Bike360.Application.Features.DivingSchoolCustomers.Queries.GetCustomerCourseDetails;

public class GetCustomerCourseDetailsQueryHandler : IRequestHandler<GetCustomerCourseDetailsQuery, CustomerCourseDetailsDto>
{
    private readonly ICustomersDivingCoursesRelationsRepository _relationsRepository;
    private readonly IMapper _mapper;

    public GetCustomerCourseDetailsQueryHandler(
        ICustomersDivingCoursesRelationsRepository relationsRepository,
        IMapper mapper)
    {
        _relationsRepository = relationsRepository;
        _mapper = mapper;
    }

    public async Task<CustomerCourseDetailsDto> Handle(
        GetCustomerCourseDetailsQuery request,
        CancellationToken cancellationToken)
    {
        var details = await _relationsRepository.GetCustomerCourseDetails(request.customerId, request.courseId) ??
                     throw new NotFoundException($"Relation with customerId: {request.customerId} and courseId:", request.courseId);

        var data = _mapper.Map<CustomerCourseDetailsDto>(details);

        return data;
    }
}
