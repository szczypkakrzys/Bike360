using MediatR;

namespace Bike360.Application.Features.Bikes.Commands.UpdateBike;

public class UpdateBikeCommand : IRequest<Unit>
{
    public int Id { get; set; }
    public string Brand { get; set; }
    public string Name { get; set; }
    public string? FrameNumber { get; set; }
}
