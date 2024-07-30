using AutoMapper;
using CustomersManagement.Application.Contracts.Persistence;
using CustomersManagement.Application.Exceptions;
using CustomersManagement.Application.Features.Tours.Commands.CreateTour;
using CustomersManagement.Domain.TravelAgency;
using FluentAssertions;
using FluentValidation.TestHelper;
using NSubstitute;

namespace CustomersManagement.Application.UnitTests.Features.Tours.Commands;

public class CreateTourTests
{
    private readonly ITourRepository _tourRepository;
    private readonly IMapper _mapper;
    private readonly CreateTourCommandHandler _handler;
    private readonly CreateTourCommandValidator _validator;

    public CreateTourTests()
    {
        _tourRepository = Substitute.For<ITourRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new CreateTourCommandHandler(_mapper, _tourRepository);
        _validator = new CreateTourCommandValidator();
    }

    [Fact]
    public async Task Handle_ValidRequest_ReturnsCreatedTourId()
    {
        // Arrange
        var tourId = 1;
        var tourToCreate = new Tour { Id = tourId };
        var date = DateTime.UtcNow;

        var request = new CreateTourCommand
        {
            Name = "TestTour",
            TimeStart = date,
            TimeEnd = date.AddDays(5),
            EntireCost = 500,
            AdvancePaymentCost = 150,
            EntireAmountPaymentDeadline = date.AddDays(3),
            AdvancePaymentDeadline = date.AddDays(-1),
            Status = "unknow"
        };

        _mapper.Map<Tour>(request).Returns(tourToCreate);
        _tourRepository.CreateAsync(tourToCreate).Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.Should().Be(tourId);
    }

    [Fact]
    public async Task Validate_TourDataIsEmpty_ThrowsBadRequestExceptionAndShouldHaveValidationErrors()
    {
        // Arrange
        var request = new CreateTourCommand();

        // Act
        var result = await _validator.TestValidateAsync(request);
        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<BadRequestException>()
            .WithMessage("Invalid Tour");

        await _tourRepository.DidNotReceive().CreateAsync(Arg.Any<Tour>());

        result.ShouldHaveValidationErrorFor(request => request.Name)
            .WithErrorMessage("Name is required");
        result.ShouldHaveValidationErrorFor(request => request.TimeStart)
            .WithErrorMessage("Time Start is required");
        result.ShouldHaveValidationErrorFor(request => request.TimeEnd)
            .WithErrorMessage("Time End is required");
        result.ShouldHaveValidationErrorFor(request => request.EntireCost)
            .WithErrorMessage("Entire Cost is required");
        result.ShouldHaveValidationErrorFor(request => request.AdvancePaymentCost)
            .WithErrorMessage("Advance Payment Cost is required");
        result.ShouldHaveValidationErrorFor(request => request.EntireAmountPaymentDeadline)
            .WithErrorMessage("Entire Amount Payment Deadline is required");
        result.ShouldHaveValidationErrorFor(request => request.AdvancePaymentDeadline)
            .WithErrorMessage("Advance Payment Deadline is required");
        result.ShouldHaveValidationErrorFor(request => request.Status)
            .WithErrorMessage("Status is required");
    }

    [Fact]
    public async Task Validate_CostsValuesAreNegative_ThrowsBadRequestExceptionAndShouldHaveValidationErrors()
    {
        // Arrange
        var date = DateTime.UtcNow;

        var request = new CreateTourCommand
        {
            Name = "TestTour",
            TimeStart = date,
            TimeEnd = date.AddDays(5),
            EntireCost = -500,
            AdvancePaymentCost = -150,
            EntireAmountPaymentDeadline = date.AddDays(3),
            AdvancePaymentDeadline = date.AddDays(-1),
            Status = "unknow"
        };

        // Act
        var result = await _validator.TestValidateAsync(request);
        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<BadRequestException>()
            .WithMessage("Invalid Tour");

        await _tourRepository.DidNotReceive().CreateAsync(Arg.Any<Tour>());

        result.ShouldHaveValidationErrorFor(request => request.EntireCost)
            .WithErrorMessage("Entire Cost must be greater than or equal to 0");
        result.ShouldHaveValidationErrorFor(request => request.AdvancePaymentCost)
          .WithErrorMessage("Advance Payment Cost must be greater than or equal to 0");
    }

    [Fact]
    public async Task Validate_EndDateIsBeforeStartDate_ThrowsBadRequestExceptionAndShouldHaveValidationErrors()
    {
        // Arrange
        var date = DateTime.UtcNow;

        var request = new CreateTourCommand
        {
            Name = "TestTour",
            TimeStart = date,
            TimeEnd = date.AddDays(-5),
            EntireCost = 500,
            AdvancePaymentCost = 150,
            EntireAmountPaymentDeadline = date.AddDays(-2),
            AdvancePaymentDeadline = date.AddDays(1),
            Status = "unknow"
        };

        // Act
        var result = await _validator.TestValidateAsync(request);
        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<BadRequestException>()
            .WithMessage("Invalid Tour");

        await _tourRepository.DidNotReceive().CreateAsync(Arg.Any<Tour>());

        result.ShouldHaveValidationErrorFor(request => request.TimeStart)
            .WithErrorMessage("TimeStart must be before TimeEnd");
        result.ShouldHaveValidationErrorFor(request => request.AdvancePaymentDeadline)
            .WithErrorMessage("AdvancePaymentDeadline must be before or the same time as EntireAmountPaymentDeadline");
        result.ShouldHaveValidationErrorFor(request => request.AdvancePaymentDeadline)
            .WithErrorMessage("AdvancePaymentDeadline must be before or the same time as TimeStart");

    }
}
