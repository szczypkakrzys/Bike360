using AutoMapper;
using Bike360.Application.Contracts.Persistence;
using Bike360.Application.Exceptions;
using Bike360.Application.Features.Reservations.Queries.GetReservationDetails;
using Bike360.Domain;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace Bike360.Application.UnitTests.Features.Reservations.Queries;

public class GetReservationDetailsTests
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetReservationDetailsQueryHandler> _logger;
    private readonly GetReservationDetailsQueryHandler _handler;

    public GetReservationDetailsTests()
    {
        _reservationRepository = Substitute.For<IReservationRepository>();
        _mapper = Substitute.For<IMapper>();
        _logger = Substitute.For<ILogger<GetReservationDetailsQueryHandler>>();
        _handler = new GetReservationDetailsQueryHandler(_reservationRepository, _mapper, _logger);
    }

    [Fact]
    public async Task Handle_WithExistingReservation_ReturnsReservationDetailsDto()
    {
        // Arrange
        var reservationId = 1;
        var request = new GetReservationDetailsQuery(reservationId);
        var reservationEntity = new Reservation
        {
            Id = reservationId
        };
        var reservationDetailsDto = new ReservationDetailsDto
        {
            DateTimeStartInUtc = new DateTime(2024, 01, 01),
            DateTimeEndInUtc = new DateTime(2024, 01, 10),
            Cost = 2000,
            Status = "Status",
            CustomerData = new CustomerDto
            {
                Id = 1,
                FirstName = "First Name",
                LastName = "Last Name",
                EmailAddress = "email@address.com",
                PhoneNumber = "1234567890"
            },
            BikesData = new List<BikeDto>
            {
                new()
                {
                    Id = 1,
                    Brand = "Brand",
                    Model = "Model",
                    Type = "Type",
                    Size = "Size",
                    RentCostPerDay = 200
                }
            }
        };

        _reservationRepository.GetReservationWithDetails(reservationId).Returns(reservationEntity);
        _mapper.Map<ReservationDetailsDto>(reservationEntity).Returns(reservationDetailsDto);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(reservationDetailsDto);
    }

    [Fact]
    public async Task Handle_WithNonexistentReservationId_ThrowsNotFoundException()
    {
        // Arrange
        var reservationId = 999;
        var request = new GetReservationDetailsQuery(reservationId);

        _reservationRepository.GetByIdAsync(reservationId).Returns(default(Reservation));

        // Act 
        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"{nameof(Reservation)} with ID = {reservationId} was not found");
    }
}
