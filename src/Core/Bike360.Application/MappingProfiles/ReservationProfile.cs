using AutoMapper;
using Bike360.Application.Features.Customers.Queries.GetCustomerReservations;
using Bike360.Application.Features.Reservations.Commands.CreateReservation;
using Bike360.Application.Features.Reservations.Events;
using Bike360.Application.Features.Reservations.Queries.GetReservationDetails;
using Bike360.Domain;

namespace Bike360.Application.MappingProfiles;

public class ReservationProfile : Profile
{
    public ReservationProfile()
    {
        CreateMap<CreateReservationCommand, Reservation>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Status, opt => opt.Ignore())
            .ForMember(dest => dest.Cost, opt => opt.Ignore())
            .ForMember(dest => dest.Customer, opt => opt.Ignore())
            .ForMember(dest => dest.Bikes, opt => opt.Ignore())
            .ForMember(dest => dest.ReservationBikes, opt => opt.Ignore())
            .ForMember(dest => dest.DateTimeEndInUtc, opt => opt.Ignore())
            .ForMember(dest => dest.TimeCreatedInUtc, opt => opt.Ignore())
            .ForMember(dest => dest.TimeLastModifiedInUtc, opt => opt.Ignore());

        CreateMap<Reservation, ReservationDto>();

        CreateMap<Reservation, ReservationDetailsDto>()
            .ForMember(dest => dest.CustomerData, opt => opt.MapFrom(src => src.Customer))
            .ForMember(dest => dest.BikesData, opt => opt.MapFrom(src => src.Bikes));

        CreateMap<Customer, CustomerDto>();
        CreateMap<Bike, BikeDto>();

        CreateMap<Reservation, ReservationCreatedEvent>();
        CreateMap<Reservation, ReservationDeletedEvent>();
    }
}
