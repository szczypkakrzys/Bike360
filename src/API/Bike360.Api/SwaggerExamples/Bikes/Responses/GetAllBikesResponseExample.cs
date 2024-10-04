using Bike360.Application.Features.Bikes.Queries.GetAllBikes;
using Swashbuckle.AspNetCore.Filters;

namespace Bike360.Api.SwaggerExamples.Bikes.Responses;

public class GetAllBikesResponseExample : IExamplesProvider<IEnumerable<BikeDto>>
{
    public IEnumerable<BikeDto> GetExamples()
    {
        return
            [
                new()
                {
                    Id = 1,
                    Brand = "Canyon",
                    Type = "Gravel",
                    Model = "Grail",
                    RentCostPerDay = 250
                },
                new()
                {
                    Id = 2,
                    Brand = "Canyon",
                    Type = "Road",
                    Model = "Aeroad",
                    RentCostPerDay = 400
                }
            ];
    }
}
