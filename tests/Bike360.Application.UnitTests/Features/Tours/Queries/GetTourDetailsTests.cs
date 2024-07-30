using AutoMapper;
using CustomersManagement.Application.Contracts.Persistence;
using CustomersManagement.Application.Exceptions;
using CustomersManagement.Application.Features.Tours.Queries.GetTourDetails;
using CustomersManagement.Domain.TravelAgency;
using FluentAssertions;
using NSubstitute;

namespace CustomersManagement.Application.UnitTests.Features.Tours.Queries;

public class GetTourDetailsTests
{
    private readonly ITourRepository _tourRepository;
    private readonly IMapper _mapper;
    private readonly GetTourDetailsQueryHandler _handler;

    public GetTourDetailsTests()
    {
        _tourRepository = Substitute.For<ITourRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new GetTourDetailsQueryHandler(_mapper, _tourRepository);
    }

    [Fact]
    public async Task Handle_WithExisingId_ReturnsTourDetailsDto()
    {
        // Arrange
        var tourId = 1;
        var request = new GetTourDetailsQuery(tourId);
        var tourDetails = new Tour { Id = tourId };
        var date = new DateOnly(2000, 01, 01);
        var tourDetailsDto = new TourDetailsDto
        {
            Id = tourId,
            Name = "TestTour",
            TimeStart = date.ToString(),
            TimeEnd = date.AddDays(5).ToString(),
            EntireCost = 500,
            AdvancePaymentCost = 150,
            EntireAmountPaymentDeadline = date.AddDays(3).ToString(),
            AdvancePaymentDeadline = date.AddDays(-1).ToString(),
        };

        _tourRepository.GetByIdAsync(tourId).Returns(tourDetails);
        _mapper.Map<TourDetailsDto>(tourDetails).Returns(tourDetailsDto);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(tourDetailsDto);
    }

    [Fact]
    public async Task Handle_WithNonexistentTourId_ThrowsNotFoundException()
    {
        // Arrange
        var tourId = 999;
        var request = new GetTourDetailsQuery(tourId);

        _tourRepository.GetByIdAsync(tourId).Returns(default(Tour));

        // Act
        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"{nameof(Tour)} ({tourId}) was not found");
    }
}
