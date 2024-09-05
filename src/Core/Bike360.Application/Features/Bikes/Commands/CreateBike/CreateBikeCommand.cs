using MediatR;

namespace Bike360.Application.Features.Bikes.Commands.CreateBike;

public class CreateBikeCommand : IRequest<int>
{
    public string Brand { get; set; }
    public string Type { get; set; }
    public string Model { get; set; }
    public string Size { get; set; }
    public string Color { get; set; }
    public double RentCostPerDay { get; set; }
    public string? FrameNumber { get; set; }
    public string? Description { get; set; }
}
