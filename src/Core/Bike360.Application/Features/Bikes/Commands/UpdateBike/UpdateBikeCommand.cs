using MediatR;

namespace Bike360.Application.Features.Bikes.Commands.UpdateBike;

public class UpdateBikeCommand : IRequest<Unit>
{
    public int Id { get; set; }
    public string Brand { get; set; }
    public string Type { get; set; }
    public string Model { get; set; }
    public string Size { get; set; }
    public string Color { get; set; }
    public string? FrameNumber { get; set; }
    public string? Description { get; set; }
}
