using Bike360.Application.Features.Customers.Commands.UpdateCustomer;
using Bike360.Application.Features.Customers.Shared;
using Swashbuckle.AspNetCore.Filters;

namespace Bike360.Api.SwaggerExamples.Customers.Requests;

public class UpdateCustomerCommandExample : IExamplesProvider<UpdateCustomerCommand>
{
    public UpdateCustomerCommand GetExamples()
    {
        return new UpdateCustomerCommand()
        {
            Id = 1,
            FirstName = "Geralt",
            LastName = "Of Rivia",
            EmailAddress = "geralt@woolfschool.com",
            PhoneNumber = "000000000",
            DateOfBirth = new DateTime(0001, 1, 1),
            Address = new AddressDto
            {
                Country = "Toussaint",
                Voivodeship = "Sansretour Valley",
                PostalCode = "12-345",
                City = "Corvo Bianco",
                Street = "Wine St",
                HouseNumber = "1"
            }
        };
    }
}
