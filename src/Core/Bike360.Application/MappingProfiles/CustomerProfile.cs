using AutoMapper;
using Bike360.Application.Features.Customers.Commands.CreateCustomer;
using Bike360.Application.Features.DivingSchoolCustomers.Commands.CreateDivingSchoolCustomer;
using Bike360.Application.Features.DivingSchoolCustomers.Commands.UpdateDivingSchoolCustomer;
using Bike360.Application.Features.DivingSchoolCustomers.Queries.GetAllDivingSchoolCustomers;
using Bike360.Application.Features.DivingSchoolCustomers.Queries.GetDivingSchoolCustomerDetails;
using Bike360.Application.Features.Shared;
using Bike360.Application.MappingProfiles.Customs;
using Bike360.Domain;

namespace Bike360.Application.MappingProfiles;

public class CustomerProfile : Profile
{
    public CustomerProfile()
    {
        CreateMap<Customer, CustomerDto>();
        CreateMap<Customer, CustomerDetailsDto>();
        CreateMap<CreateCustomerCommand, Customer>()
            .ForMember(dest => dest.AddressId, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Reservations, opt => opt.Ignore())
            .ForMember(dest => dest.TimeCreatedInUtc, opt => opt.Ignore())
            .ForMember(dest => dest.TimeLastModifiedInUtc, opt => opt.Ignore());
        CreateMap<UpdateCustomerCommand, Customer>()
            .ForMember(dest => dest.TimeCreatedInUtc, opt => opt.Ignore())
            .ForMember(dest => dest.Reservations, opt => opt.Ignore())
            .ForMember(dest => dest.TimeLastModifiedInUtc, opt => opt.Ignore());
        CreateMap<CreateAddressDto, Address>()
            .ForMember(dest => dest.Customer, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.TimeCreatedInUtc, opt => opt.Ignore())
            .ForMember(dest => dest.TimeLastModifiedInUtc, opt => opt.Ignore());
        CreateMap<Address, AddressDto>().ReverseMap();
        //TODO
        // probably don't use it anymore
        CreateMap<DateTime, DateOnly>().ConvertUsing(new DateTimeToDateOnlyConverter());
    }
}
