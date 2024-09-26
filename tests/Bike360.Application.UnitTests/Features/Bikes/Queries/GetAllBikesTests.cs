using AutoMapper;
using Bike360.Application.Contracts.Persistence;
using Bike360.Application.Features.Bikes.Queries.GetAllBikes;
using Bike360.Domain;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace Bike360.Application.UnitTests.Features.Bikes.Queries;

public class GetAllBikesTests
{
    private readonly IBikeRepository _bikeRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetAllBikesQueryHandler> _logger;
    private readonly GetAllBikesQueryHandler _handler;

    public GetAllBikesTests()
    {
        _bikeRepository = Substitute.For<IBikeRepository>();
        _mapper = Substitute.For<IMapper>();
        _logger = Substitute.For<ILogger<GetAllBikesQueryHandler>>();
        _handler = new GetAllBikesQueryHandler(_bikeRepository, _mapper, _logger);
    }

    [Fact]
    public async Task Handle_WithValidRequest_ReturnsListOfBikeDto()
    {
        // Arrange
        var request = new GetAllBikesQuery();
        var bikes = new List<Bike>();
        var expected = new List<BikeDto>
        {
            new()
            {
                Id = 1,
                Brand = "Brand 1",
                Type = "Road",
                Model = "Model 1",
                RentCostPerDay = 200
            },
            new()
            {
                Id = 2,
                Brand = "Brand 2",
                Type = "Road",
                Model = "Model 2",
                RentCostPerDay = 200
            }
        };

        _bikeRepository.GetAsync().Returns(bikes);
        _mapper.Map<IEnumerable<BikeDto>>(bikes).Returns(expected);

        // Act 
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(expected);
    }
}
