namespace Bike360.Domain;

public abstract class BaseEntity
{
    //TODO - use those after adding auth to project
    public int Id { get; set; }
    public DateTime TimeCreatedInUtc { get; set; }
    //public string? CreatedBy { get; set; }
    public DateTime TimeLastModifiedInUtc { get; set; }
    //public string? LastModifiedBy { get; set; }
}