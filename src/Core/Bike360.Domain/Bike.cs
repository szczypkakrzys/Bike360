namespace Bike360.Domain;

public class Bike : BaseEntity
{
    public string Brand { get; set; }
    public string Name { get; set; }
    public string? FrameNumber { get; set; }
    public int? CustomerId { get; set; }
    public Customer Customer { get; set; }
}
