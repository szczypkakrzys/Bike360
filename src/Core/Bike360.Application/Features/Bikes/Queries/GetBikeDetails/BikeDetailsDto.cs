namespace Bike360.Application.Features.Bikes.Queries.GetBikeDetails;

public class BikeDetailsDto
{
    public int Id { get; set; }
    public string Brand { get; set; }
    public string Name { get; set; }
    public string? FrameNumber { get; set; }
    //TODO
    //owner's data 
    //repairs history 
}
