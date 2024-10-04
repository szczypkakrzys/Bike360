using MediatR;

namespace Bike360.Application.Features.Bikes.Commands.DeleteBike;

public class DeleteBikeCommand : IRequest<Unit>
{
    public int Id { get; set; }
}
