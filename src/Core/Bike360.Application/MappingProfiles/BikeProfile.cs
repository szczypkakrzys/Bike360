using AutoMapper;
using Bike360.Application.Features.Bikes.Commands.CreateBike;
using Bike360.Application.Features.Bikes.Commands.UpdateBike;
using Bike360.Application.Features.Bikes.Queries.GetAllBikes;
using Bike360.Application.Features.Bikes.Queries.GetBikeDetails;
using Bike360.Domain;

namespace Bike360.Application.MappingProfiles;

public class BikeProfile : Profile
{
    public BikeProfile()
    {
        CreateMap<CreateBikeCommand, Bike>()
            .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(source => source.OwnerId))
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Customer, opt => opt.Ignore())
            .ForMember(dest => dest.TimeCreatedInUtc, opt => opt.Ignore())
            .ForMember(dest => dest.TimeLastModifiedInUtc, opt => opt.Ignore());
        CreateMap<UpdateBikeCommand, Bike>()
            .ForMember(dest => dest.CustomerId, opt => opt.Ignore())
            .ForMember(dest => dest.Customer, opt => opt.Ignore())
            .ForMember(dest => dest.TimeCreatedInUtc, opt => opt.Ignore())
            .ForMember(dest => dest.TimeLastModifiedInUtc, opt => opt.Ignore());
        CreateMap<Bike, BikeDto>();
        CreateMap<Bike, BikeDetailsDto>();
    }
}
