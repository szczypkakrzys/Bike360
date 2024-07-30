using AutoMapper;
using Bike360.UI.Models;
using Bike360.UI.Models.DivingSchool;
using Bike360.UI.Models.Emloyee;
using Bike360.UI.Models.Newsletter;
using Bike360.UI.Models.Notification;
using Bike360.UI.Models.Shared;
using Bike360.UI.Models.TravelAgency;
using Bike360.UI.Services.Base;

namespace Bike360.UI.MappingProfiles;

public class MappingConfig : Profile
{
    public MappingConfig()
    {
        CreateMap<TravelAgencyCustomerDto, CustomerVM>();
        CreateMap<TravelAgencyCustomerDetailsDto, TravelAgencyCustomerDetailsVM>();
        CreateMap<AddressDto, AddressVM>().ReverseMap();
        CreateMap<AddressVM, CreateAddressDto>();
        CreateMap<TravelAgencyCustomerDetailsVM, CreateTravelAgencyCustomerCommand>();
        CreateMap<TravelAgencyCustomerDetailsVM, UpdateTravelAgencyCustomerCommand>();
        CreateMap<CustomerTourDto, CustomerActivityVM>();
        CreateMap<CustomerTourDetailsDto, CustomerActivityDetailsVM>();

        CreateMap<TourDetailsVM, CreateTourCommand>();
        CreateMap<TourDto, ActivityVM>();
        CreateMap<TourDetailsDto, TourDetailsVM>();
        CreateMap<TourDetailsVM, UpdateTourCommand>();
        CreateMap<TourParticipantDto, ActivityParticipantVM>();

        CreateMap<DivingSchoolCustomerDetailsVM, CreateDivingSchoolCustomerCommand>();
        CreateMap<DivingSchoolCustomerDto, CustomerVM>();
        CreateMap<DivingSchoolCustomerDetailsDto, DivingSchoolCustomerDetailsVM>();
        CreateMap<DivingSchoolCustomerDetailsVM, UpdateDivingSchoolCustomerCommand>();
        CreateMap<CustomerCourseDto, CustomerActivityVM>();
        CreateMap<CustomerCourseDetailsDto, CustomerActivityDetailsVM>();

        CreateMap<CourseDetailsVM, CreateCourseCommand>();
        CreateMap<CourseDto, ActivityVM>();
        CreateMap<CourseDetailsDto, CourseDetailsVM>();
        CreateMap<CourseParticipantDto, ActivityParticipantVM>();
        CreateMap<CourseDetailsVM, UpdateTourCommand>();

        CreateMap<EmailVM, SendEmailCommand>();

        CreateMap<Employee, EmployeeVM>();
        CreateMap<RegisterVM, RegisterNewUserCommand>();

        CreateMap<NotificationDto, NotificationVM>()
            .ForMember(dest => dest.ViewMessage, opt => opt.Ignore())
            .ForMember(dest => dest.Customers, opt => opt.Ignore());
    }
}
