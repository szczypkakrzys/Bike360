namespace Bike360.Domain;

public class Customer : BaseEntity
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public int AddressId { get; set; }
    public Address Address { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string EmailAddress { get; set; } = string.Empty;
    public string PhoneNumber { get; set; }
}
