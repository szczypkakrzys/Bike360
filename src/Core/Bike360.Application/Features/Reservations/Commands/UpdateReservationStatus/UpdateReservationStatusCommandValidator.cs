using Bike360.Application.Features.Reservations.Constants;
using FluentValidation;

namespace Bike360.Application.Features.Reservations.Commands.UpdateReservationStatus;

public class UpdateReservationStatusCommandValidator : AbstractValidator<UpdateReservationStatusCommand>
{
    public UpdateReservationStatusCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
                .WithMessage("{PropertyName} is required");

        RuleFor(x => x.Status)
            .NotEmpty()
                .WithMessage("{PropertyName} is required")
            .Must(BeAValidStatus)
                .WithMessage(StatusMessage());
    }

    private bool BeAValidStatus(string status)
    {
        return AvailableStatuses.Contains(status);
    }

    private readonly string[] AvailableStatuses =
        [
            ReservationStatus.Pending
        ];

    private string StatusMessage()
    {
        var statuses = string.Join(", ", AvailableStatuses);
        return $"The status value is not valid. It must be one of the following: {statuses}.";
    }

}
