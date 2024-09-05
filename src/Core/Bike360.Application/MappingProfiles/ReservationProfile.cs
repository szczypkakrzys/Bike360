using AutoMapper;
using Bike360.Application.Features.Reservations.Commands.CreateReservation;
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
            .ForMember(dest => dest.DateTimeEnd, opt => opt.Ignore())
            .ForMember(dest => dest.TimeCreatedInUtc, opt => opt.Ignore())
            .ForMember(dest => dest.TimeLastModifiedInUtc, opt => opt.Ignore());
    }
}
