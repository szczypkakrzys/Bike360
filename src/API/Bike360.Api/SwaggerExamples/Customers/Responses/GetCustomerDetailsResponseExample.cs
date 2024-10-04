using Bike360.Application.Features.Customers.Queries.GetCustomerDetails;
using Bike360.Application.Features.Customers.Shared;
using Swashbuckle.AspNetCore.Filters;

namespace Bike360.Api.SwaggerExamples.Customers.Responses;

public class GetCustomerDetailsResponseExample : IExamplesProvider<CustomerDetailsDto>
{
    public CustomerDetailsDto GetExamples()
    {
        return new CustomerDetailsDto()
        {
            Id = 1,
            FirstName = "Geralt",
            LastName = "Of Rivia",
            EmailAddress = "geralt@woolfschool.com",
            PhoneNumber = "000000000",
            DateOfBirth = new DateOnly(0001, 1, 1).ToString(),
            Address = new AddressDto
            {
                Id = 1,
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
