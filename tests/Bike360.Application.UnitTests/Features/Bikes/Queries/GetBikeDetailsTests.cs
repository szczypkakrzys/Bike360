using AutoMapper;
using Bike360.Application.Contracts.Persistence;
using Bike360.Application.Exceptions;
using Bike360.Application.Features.Bikes.Queries.GetBikeDetails;
using Bike360.Domain;
using FluentAssertions;
using NSubstitute;

namespace Bike360.Application.UnitTests.Features.Bikes.Queries;

public class GetBikeDetailsTests
{
    public readonly IBikeRepository _bikeRepository;
    public readonly IMapper _mapper;
    public readonly GetBikeDetailsQueryHandler _handler;

    public GetBikeDetailsTests()
    {
        _bikeRepository = Substitute.For<IBikeRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new GetBikeDetailsQueryHandler(_bikeRepository, _mapper);
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
            Brand = "Brand",
            Name = "Name",
            FrameNumber = "0123456789"
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
            .WithMessage($"{nameof(Bike)} ({bikeId}) was not found");
    }
}
