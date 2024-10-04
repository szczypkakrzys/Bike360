using Bike360.Application.Features.Bikes.Queries.GetBikeDetails;
using Swashbuckle.AspNetCore.Filters;

namespace Bike360.Api.SwaggerExamples.Bikes.Responses;

public class GetBikeDetailsResponseExample : IExamplesProvider<BikeDetailsDto>
{
    public BikeDetailsDto GetExamples()
    {
        return new BikeDetailsDto()
        {
            Id = 1,
            Brand = "Canyon",
            Model = "Grail",
            Type = "Gravel",
            Size = "XL",
            RentCostPerDay = 250,
            Color = "Goodwood Green",
            FrameNumber = "0123456789",
            Description = "A really fast bike with a stunning color :)"
        };
    }
}
