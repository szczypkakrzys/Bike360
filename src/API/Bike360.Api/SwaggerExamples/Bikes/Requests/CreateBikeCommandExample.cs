using Bike360.Application.Features.Bikes.Commands.CreateBike;
using Swashbuckle.AspNetCore.Filters;

namespace Bike360.Api.SwaggerExamples.Bikes.Requests;

public class CreateBikeCommandExample : IExamplesProvider<CreateBikeCommand>
{
    public CreateBikeCommand GetExamples()
    {
        return new CreateBikeCommand
        {
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
