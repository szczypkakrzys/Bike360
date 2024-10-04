using Bike360.Domain.Models;
using Swashbuckle.AspNetCore.Filters;

namespace Bike360.Api.SwaggerExamples.Bikes.Responses;

public class GetBikeReservedDaysResponseExample : IExamplesProvider<List<DateRange>>
{
    public List<DateRange> GetExamples()
    {
        return
            [
                new(new DateTime(2024, 10, 1, 1, 1, 1), new DateTime(2024, 10, 5, 1, 1, 1)),
                new(new DateTime(2024, 10, 15, 1, 1, 1), new DateTime(2024, 10, 25, 1, 1, 1))
            ];
    }
}
