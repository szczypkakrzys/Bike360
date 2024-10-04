using Bike360.Application.Features.Reservations.Commands.UpdateReservationStatus;
using Swashbuckle.AspNetCore.Filters;

namespace Bike360.Api.SwaggerExamples.Reservations.Requests;

public class UpdateReservationStatusCommandExample : IExamplesProvider<UpdateReservationStatusCommand>
{
    public UpdateReservationStatusCommand GetExamples()
    {
        return new UpdateReservationStatusCommand()
        {
            Id = 1,
            Status = "Completed"
        };
    }
}
