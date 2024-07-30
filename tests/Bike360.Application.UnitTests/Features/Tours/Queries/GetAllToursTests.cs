using AutoMapper;
using CustomersManagement.Application.Contracts.Persistence;
using CustomersManagement.Application.Features.Tours.Queries.GetAllTours;
using CustomersManagement.Domain.TravelAgency;
using FluentAssertions;
using NSubstitute;

namespace CustomersManagement.Application.UnitTests.Features.Tours.Queries;

public class GetAllToursTests
{
    private readonly ITourRepository _tourRepository;
    private readonly IMapper _mapper;
    private readonly GetToursQueryHandler _handler;

    public GetAllToursTests()
    {
        _tourRepository = Substitute.For<ITourRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new GetToursQueryHandler(_mapper, _tourRepository);
    }

    [Fact]
    public async Task Handle_WithValidRequest_ReturnsListOfCustomerDto()
    {
        // Arrange
        var request = new GetToursQuery();
        var tours = new List<Tour>();
        var date = new DateOnly(2000, 01, 01);
        var expected = new List<TourDto>()
        {
            new()
            {
                Id = 1,
                Name = "TestTour",
                TimeStart = date.ToString(),
                TimeEnd = date.AddDays(1).ToString(),
            },
            new()
            {
                Id = 2,
                Name = "TestTour2",
                TimeStart = date.ToString(),
                TimeEnd = date.AddDays(1).ToString()
            }
        };

        _tourRepository.GetAsync().Returns(tours);
        _mapper.Map<IEnumerable<TourDto>>(tours).Returns(expected);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(expected);
    }
}
