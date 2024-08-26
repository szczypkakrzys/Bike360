﻿using AutoMapper;
using Bike360.Application.Contracts.Persistence;
using Bike360.Application.Features.Bikes.Queries.GetAllBikes;
using Bike360.Domain;
using FluentAssertions;
using NSubstitute;

namespace Bike360.Application.UnitTests.Features.Bikes.Queries;

public class GetAllBikesTests
{
    private readonly IBikeRepository _bikeRepository;
    private readonly IMapper _mapper;
    private readonly GetAllBikesQueryHandler _handler;

    public GetAllBikesTests()
    {
        _bikeRepository = Substitute.For<IBikeRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new GetAllBikesQueryHandler(_bikeRepository, _mapper);
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
                Name = "Name 1",
                FrameNumber = "0123456789"
            },
            new()
            {
                Id = 2,
                Brand = "Brand 2",
                Name = "Name 2"
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
