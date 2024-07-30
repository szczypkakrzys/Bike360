using AutoMapper;
using Bike360.Application.Features.DivingSchoolCustomers.Commands.CreateDivingSchoolCustomer;
using Bike360.Application.Features.DivingSchoolCustomers.Commands.UpdateDivingSchoolCustomer;
using Bike360.Application.Features.DivingSchoolCustomers.Queries.GetAllCustomerCourses;
using Bike360.Application.Features.DivingSchoolCustomers.Queries.GetAllDivingSchoolCustomers;
using Bike360.Application.Features.DivingSchoolCustomers.Queries.GetCustomerCourseDetails;
using Bike360.Application.Features.DivingSchoolCustomers.Queries.GetDivingSchoolCustomerDetails;
using Bike360.Domain.DivingSchool;

namespace Bike360.Application.MappingProfiles;

public class DivingSchoolCustomerProfile : Profile
{
    public DivingSchoolCustomerProfile()
    {
        CreateMap<DivingSchoolCustomer, DivingSchoolCustomerDto>();
        CreateMap<DivingSchoolCustomer, DivingSchoolCustomerDetailsDto>();
        CreateMap<CreateDivingSchoolCustomerCommand, DivingSchoolCustomer>();
        CreateMap<UpdateDivingSchoolCustomerCommand, DivingSchoolCustomer>();
        CreateMap<DivingCourse, CustomerCourseDto>();
        CreateMap<CustomersDivingCoursesRelations, CustomerCourseDetailsDto>();
    }
}
