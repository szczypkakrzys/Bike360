using AutoMapper;
using Bike360.Application.Contracts.Persistence;
using Bike360.Application.Exceptions;
using Bike360.Application.Features.Reservations.Queries.GetCustomerReservations;
using Bike360.Domain;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace Bike360.Application.UnitTests.Features.Reservations.Queries;

public class GetCustomerReservationsTests
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IReservationRepository _reservationRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetCustomerReservationsQueryHandler> _logger;
    private readonly GetCustomerReservationsQueryHandler _handler;

    public GetCustomerReservationsTests()
    {
        _customerRepository = Substitute.For<ICustomerRepository>();
        _reservationRepository = Substitute.For<IReservationRepository>();
        _mapper = Substitute.For<IMapper>();
        _logger = Substitute.For<ILogger<GetCustomerReservationsQueryHandler>>();
        _handler = new GetCustomerReservationsQueryHandler(_customerRepository, _reservationRepository, _mapper, _logger);
    }

    [Fact]
    public async Task Handle_WithExistingCustomer_ReturnsListOfReservations()
    {
        // Arrange
        var customerId = 1;
        var customerEntity = new Customer { Id = customerId };
        var request = new GetCustomerReservationsQuery(customerId);
        var reservations = new List<Reservation>();
        var reservationsDtos = new List<ReservationDto>
        {
            new()
            {
                DateTimeStartInUtc = new DateTime(2024, 01, 01),
                DateTimeEndInUtc = new DateTime(2024, 01, 10),
                Cost = 2000,
                Comments = "Some comment",
                Status = "Some status"
            }
        };

        _customerRepository.GetByIdAsync(customerId).Returns(customerEntity);
        _reservationRepository.GetAllCustomerReservations(customerId).Returns(reservations);
        _mapper.Map<IEnumerable<ReservationDto>>(reservations).Returns(reservationsDtos);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(reservationsDtos);
    }

    [Fact]
    public async Task Handle_WithNonexistentCustomerId_ThrowsNotFoundExcpetion()
    {
        // Arrange
        var customerId = 999;
        var request = new GetCustomerReservationsQuery(customerId);

        _customerRepository.GetByIdAsync(customerId).Returns(default(Customer));

        // Act
        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"{nameof(Customer)} with ID = {customerId} was not found");
    }
}
