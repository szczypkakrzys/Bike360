using MediatR;

namespace Bike360.Application.Features.Bikes.Commands.CreateBike;

public class CreateBikeCommand : IRequest<int>
{
    public string Brand { get; set; }
    public string Name { get; set; }
    public string? FrameNumber { get; set; }
    public int OwnerId { get; set; }
}
