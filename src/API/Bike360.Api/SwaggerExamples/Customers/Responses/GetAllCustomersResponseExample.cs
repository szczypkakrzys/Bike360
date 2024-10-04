using Bike360.Application.Features.Customers.Queries.GetAllCustomers;
using Swashbuckle.AspNetCore.Filters;

namespace Bike360.Api.SwaggerExamples.Customers.Responses;

public class GetAllCustomersResponseExample : IExamplesProvider<IEnumerable<CustomerDto>>
{
    public IEnumerable<CustomerDto> GetExamples()
    {
        return
            [
                new()
                {
                    Id = 1,
                    FirstName = "Geralt",
                    LastName = "Of Rivia",
                    EmailAddress = "geralt@woolfschool.com"
                },
                new()
                {
                    Id = 2,
                    FirstName = "Emiel",
                    LastName = "Regis",
                    EmailAddress = "regis@highervampire.com"
                }
            ];
    }
}
