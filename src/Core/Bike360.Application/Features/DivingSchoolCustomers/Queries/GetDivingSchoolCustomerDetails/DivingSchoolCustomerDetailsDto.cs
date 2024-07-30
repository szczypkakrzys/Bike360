using Bike360.Application.Features.Shared;

namespace Bike360.Application.Features.DivingSchoolCustomers.Queries.GetDivingSchoolCustomerDetails;

public class DivingSchoolCustomerDetailsDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string EmailAddress { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string DateOfBirth { get; set; }
    public string DivingCertificationLevel { get; set; }
    public AddressDto Address { get; set; }
}
