using Bike360.Application.Features.Reservations.Commands.CreateReservation;
using Swashbuckle.AspNetCore.Filters;

namespace Bike360.Api.SwaggerExamples.Reservations.Requests;

public class CreateReservationCommandExample : IExamplesProvider<CreateReservationCommand>
{
    public CreateReservationCommand GetExamples()
    {
        return new CreateReservationCommand()
        {
            DateTimeStartInUtc = DateTime.Now.AddDays(1),
            NumberOfDays = 1,
            Comments = "Reservation comments",
            CustomerId = 1,
            BikesIds = new[] { 1, 2, 3 }
        };
    }
}
