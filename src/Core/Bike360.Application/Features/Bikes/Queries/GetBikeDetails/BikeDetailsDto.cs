namespace Bike360.Application.Features.Bikes.Queries.GetBikeDetails;

public class BikeDetailsDto
{
    public int Id { get; set; }
    public string Brand { get; set; }
    public string Type { get; set; }
    public string Model { get; set; }
    public string Size { get; set; }
    public string Color { get; set; }
    // TODO
    // frameNumber - probably this info is more importa for manager or customer support?
    public string? FrameNumber { get; set; }
    public string? Description { get; set; }
}
