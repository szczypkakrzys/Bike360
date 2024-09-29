using Bike360.Application.Features.Customers.Shared;
using MediatR;

namespace Bike360.Application.Features.Customers.Commands.UpdateCustomer;

public class UpdateCustomerCommand : IRequest<Unit>
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string EmailAddress { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime DateOfBirth { get; set; }
    public AddressDto Address { get; set; }
}
