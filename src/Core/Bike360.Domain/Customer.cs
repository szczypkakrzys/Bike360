namespace Bike360.Domain;

public class Customer : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int AddressId { get; set; }
    public Address Address { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string EmailAddress { get; set; }
    public string PhoneNumber { get; set; }
    public ICollection<Bike> Bikes { get; } = new List<Bike>();
}
