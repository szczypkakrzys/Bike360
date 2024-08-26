using AutoMapper;
using Bike360.Application.Contracts.Persistence;
using Bike360.Application.Exceptions;
using Bike360.Application.Features.Bikes.Commands.CreateBike;
using Bike360.Domain;
using FluentAssertions;
using FluentValidation.TestHelper;
using NSubstitute;

namespace Bike360.Application.UnitTests.Features.Bikes.Commands;

public class CreateBikeTests
{
    private readonly IMapper _mapper;
    private readonly IBikeRepository _bikeRepository;
    private readonly CreateBikeCommandHandler _handler;
    private readonly CreateBikeCommandValidator _validator;

    public CreateBikeTests()
    {
        _mapper = Substitute.For<IMapper>();
        _bikeRepository = Substitute.For<IBikeRepository>();
        _handler = new CreateBikeCommandHandler(_mapper, _bikeRepository);
        _validator = new CreateBikeCommandValidator();
    }

    [Fact]
    public async Task Handle_ValidRequest_ReturnsCreatedBikeId()
    {
        // Arrange
        var bikeId = 1;
        var bikeToCreate = new Bike { Id = bikeId };

        var request = new CreateBikeCommand
        {
            Brand = "AnyBrand",
            Name = "AnyName",
            FrameNumber = "0123456789"
        };

        _mapper.Map<Bike>(request).Returns(bikeToCreate);
        _bikeRepository.CreateAsync(bikeToCreate).Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.Should().Be(bikeId);
    }

    [Fact]
    public async Task Validate_BikeDataIsEmpty_ThrowsBadRequestExceptionAndShouldHaveValidationErrors()
    {
        // Arrange
        var request = new CreateBikeCommand();

        // Act
        var result = await _validator.TestValidateAsync(request);
        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<BadRequestException>()
            .WithMessage("Invalid Bike");

        await _bikeRepository.DidNotReceive().CreateAsync(Arg.Any<Bike>());

        result.ShouldHaveValidationErrorFor(request => request.Brand)
            .WithErrorMessage("Brand is required");
        result.ShouldHaveValidationErrorFor(request => request.Name)
           .WithErrorMessage("Name is required");
        result.ShouldNotHaveValidationErrorFor(request => request.FrameNumber);
    }
}
