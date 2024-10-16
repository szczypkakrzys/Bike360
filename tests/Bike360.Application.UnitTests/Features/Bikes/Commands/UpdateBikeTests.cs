﻿using AutoMapper;
using Bike360.Application.Contracts.Persistence;
using Bike360.Application.Exceptions;
using Bike360.Application.Features.Bikes.Commands.UpdateBike;
using Bike360.Domain;
using FluentAssertions;
using FluentValidation.TestHelper;
using MediatR;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace Bike360.Application.UnitTests.Features.Bikes.Commands;

public class UpdateBikeTests
{
    private readonly IMapper _mapper;
    private readonly IBikeRepository _bikeRepository;
    private readonly ILogger<UpdateBikeCommandHandler> _logger;
    private readonly UpdateBikeCommandHandler _handler;
    private readonly UpdateBikeCommandValidator _validator;

    public UpdateBikeTests()
    {
        _mapper = Substitute.For<IMapper>();
        _bikeRepository = Substitute.For<IBikeRepository>();
        _logger = Substitute.For<ILogger<UpdateBikeCommandHandler>>();
        _handler = new UpdateBikeCommandHandler(_bikeRepository, _mapper, _logger);
        _validator = new UpdateBikeCommandValidator();
    }

    [Fact]
    public async Task Handle_ValidRequest_ReturnsUnitValue()
    {
        // Arrange
        var bikeId = 1;
        var request = new UpdateBikeCommand
        {
            Id = bikeId,
            Brand = "AnyBrand",
            Type = "Gravel",
            Model = "AnyModel",
            Size = "XL",
            Color = "Goodwood Green",
            RentCostPerDay = 200,
            FrameNumber = "0123456789"
        };

        var bikeToUpdate = new Bike();

        _bikeRepository.GetByIdAsync(bikeId).Returns(bikeToUpdate);
        _bikeRepository.UpdateAsync(bikeToUpdate).Returns(Task.CompletedTask);
        _mapper.Map<Bike>(request).Returns(bikeToUpdate);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.Should().Be(Unit.Value);
    }

    [Fact]
    public async Task Handle_WithNonexistentBikeId_ThrowsNotFoundExceptionAndShouldHaveIdValidationError()
    {
        // Arrange
        var bikeId = 1;
        var request = new UpdateBikeCommand
        {
            Id = bikeId,
            Brand = "AnyBrand",
            Type = "Gravel",
            Model = "AnyModel",
            Size = "XL",
            Color = "Goodwood Green",
            RentCostPerDay = 200,
            FrameNumber = "0123456789"
        };

        _bikeRepository.GetByIdAsync(bikeId).Returns(default(Bike));

        // Act
        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"{nameof(Bike)} with ID = {bikeId} was not found");

        await _bikeRepository.DidNotReceive().UpdateAsync(Arg.Any<Bike>());
    }

    [Fact]
    public async Task Validate_BikeDataIsEmpty_ThrowsBadRequestExceptionAndShouldHaveValidationErrors()
    {
        // Arrange
        var request = new UpdateBikeCommand();

        // Act
        var result = await _validator.TestValidateAsync(request);
        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<BadRequestException>()
            .WithMessage("Invalid bike data");

        await _bikeRepository.DidNotReceive().UpdateAsync(Arg.Any<Bike>());

        result.ShouldHaveValidationErrorFor(request => request.Id)
            .WithErrorMessage("Id is required");
        result.ShouldHaveValidationErrorFor(request => request.Brand)
           .WithErrorMessage("Brand is required");
        result.ShouldHaveValidationErrorFor(request => request.Type)
           .WithErrorMessage("Type is required");
        result.ShouldHaveValidationErrorFor(request => request.Model)
          .WithErrorMessage("Model is required");
        result.ShouldHaveValidationErrorFor(request => request.Size)
          .WithErrorMessage("Size is required");
        result.ShouldHaveValidationErrorFor(request => request.Color)
          .WithErrorMessage("Color is required");
        result.ShouldHaveValidationErrorFor(request => request.RentCostPerDay)
          .WithErrorMessage("Rent cost is required");
        result.ShouldNotHaveValidationErrorFor(request => request.FrameNumber);
        result.ShouldNotHaveValidationErrorFor(request => request.Description);
    }

    [Fact]
    public async Task Validate_RentCostIsNegativeValue_ThrowsBadRequestExceptionAndShouldHaveValidationErrors()
    {
        // Arrange
        var request = new UpdateBikeCommand
        {
            Brand = "AnyBrand",
            Type = "Gravel",
            Model = "AnyModel",
            Size = "XL",
            RentCostPerDay = -200,
            Color = "Goodwood Green",
            FrameNumber = "0123456789"
        };

        // Act 
        var result = await _validator.TestValidateAsync(request);
        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<BadRequestException>()
           .WithMessage("Invalid bike data");

        await _bikeRepository.DidNotReceive().UpdateAsync(Arg.Any<Bike>());

        result.ShouldHaveValidationErrorFor(request => request.RentCostPerDay)
            .WithErrorMessage("Rent cost must be greater than 0");
    }
}
