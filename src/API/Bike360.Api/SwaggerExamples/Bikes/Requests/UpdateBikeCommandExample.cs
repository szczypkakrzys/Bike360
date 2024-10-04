using Bike360.Application.Features.Bikes.Commands.UpdateBike;
using Swashbuckle.AspNetCore.Filters;

namespace Bike360.Api.SwaggerExamples.Bikes.Requests;

public class UpdateBikeCommandExample : IExamplesProvider<UpdateBikeCommand>
{
    public UpdateBikeCommand GetExamples()
    {
        return new UpdateBikeCommand()
        {
            Id = 1,
            Brand = "Canyon",
            Model = "Grail",
            Type = "Gravel",
            Size = "XL",
            RentCostPerDay = 250,
            Color = "Goodwood Green",
            FrameNumber = "0123456789",
            Description = "Updated bike data"
        };
    }
}
