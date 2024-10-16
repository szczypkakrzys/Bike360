﻿using AutoMapper;
using Bike360.Application.Contracts.Persistence;
using Bike360.Application.Exceptions;
using Bike360.Application.Features.Bikes.Queries.GetBikeDetails;
using Bike360.Domain;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace Bike360.Application.UnitTests.Features.Bikes.Queries;

public class GetBikeDetailsTests
{
    public readonly IBikeRepository _bikeRepository;
    public readonly IMapper _mapper;
    public readonly ILogger<GetBikeDetailsQueryHandler> _logger;
    public readonly GetBikeDetailsQueryHandler _handler;

    public GetBikeDetailsTests()
    {
        _bikeRepository = Substitute.For<IBikeRepository>();
        _mapper = Substitute.For<IMapper>();
        _logger = Substitute.For<ILogger<GetBikeDetailsQueryHandler>>();
        _handler = new GetBikeDetailsQueryHandler(_bikeRepository, _mapper, _logger);
    }

    [Fact]
    public async Task Handle_WithExisingId_ReturnsBikeDetailsDto()
    {
        // Arrange
        var bikeId = 1;
        var bikeDetails = new Bike();
        var bikeDetailsDto = new BikeDetailsDto
        {
            Id = 1,
            Brand = "Brand 1",
            Type = "Road",
            Model = "Model 1",
            Size = "XL",
            Color = "Monte Carlo Blue Metallic",
            RentCostPerDay = 200,
            FrameNumber = "0123456789",
            Description = "Some bike description"
        };
        var request = new GetBikeDetailsQuery(bikeId);

        _bikeRepository.GetByIdAsync(bikeId).Returns(bikeDetails);
        _mapper.Map<BikeDetailsDto>(bikeDetails).Returns(bikeDetailsDto);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(bikeDetailsDto);
    }

    [Fact]
    public async Task Handle_WithNonexistentBikeId_ThrowsNotFoundException()
    {
        // Arrange
        var bikeId = 999;
        var request = new GetBikeDetailsQuery(bikeId);

        _bikeRepository.GetByIdAsync(bikeId).Returns(default(Bike));

        // Act
        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"{nameof(Bike)} with ID = {bikeId} was not found");
    }
}
