using Bike360.Application.Features.Shared;
using MediatR;

namespace Bike360.Application.Features.DivingSchoolCustomers.Commands.CreateDivingSchoolCustomer;

public class CreateDivingSchoolCustomerCommand : IRequest<int>
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string EmailAddress { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string DivingCertificationLevel { get; set; }
    public CreateAddressDto Address { get; set; }
}
