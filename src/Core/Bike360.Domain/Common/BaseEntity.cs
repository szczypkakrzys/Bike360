namespace Bike360.Domain.Common;

public abstract class BaseEntity
{
    public int Id { get; set; }
    public DateTime TimeCreatedInUtc { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime TimeLastModifiedInUtc { get; set; }
    public string? LastModifiedBy { get; set; }
}