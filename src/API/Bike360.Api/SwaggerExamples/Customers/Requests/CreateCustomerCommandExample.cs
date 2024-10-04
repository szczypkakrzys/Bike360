using Bike360.Application.Features.Customers.Commands.CreateCustomer;
using Swashbuckle.AspNetCore.Filters;

namespace Bike360.Api.SwaggerExamples.Customers.Requests;

public class CreateCustomerCommandExample : IExamplesProvider<CreateCustomerCommand>
{
    public CreateCustomerCommand GetExamples()
    {
        return new CreateCustomerCommand()
        {
            FirstName = "Geralt",
            LastName = "Of Rivia",
            EmailAddress = "geralt@woolfschool.com",
            PhoneNumber = "000000000",
            DateOfBirth = new DateTime(0001, 1, 1),
            Address = new CreateAddressDto
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
