using Bike360.Application.Features.Reservations.Queries.GetReservationDetails;
using Swashbuckle.AspNetCore.Filters;

namespace Bike360.Api.SwaggerExamples.Reservations.Responses;

public class GetReservationDetailsResponseExample : IExamplesProvider<ReservationDetailsDto>
{
    public ReservationDetailsDto GetExamples()
    {
        return new ReservationDetailsDto()
        {
            DateTimeStartInUtc = new DateTime(2024, 10, 10, 10, 10, 10),
            DateTimeEndInUtc = new DateTime(2024, 10, 20, 10, 10, 10),
            Cost = 3000,
            Status = "Pending",
            Comments = "Reservation comment",
            CustomerData = new CustomerDto
            {
                Id = 1,
                FirstName = "Vernon",
                LastName = "Roche",
                EmailAddress = "vroche@gmail.com",
                PhoneNumber = "999999999"
            },
            BikesData = new List<BikeDto>
            {
                new()
                {
                    Id = 1,
                    Brand = "Canyon",
                    Type = "Gravel",
                    Model = "Grail",
                    RentCostPerDay = 150,
                    Size = "XL"
                },
                new()
                {
                    Id = 2,
                    Brand = "Canyon",
                    Type = "Road",
                    Model = "Aeroad",
                    RentCostPerDay = 150,
                    Size = "XL"
                }
            }
        };
    }
}
