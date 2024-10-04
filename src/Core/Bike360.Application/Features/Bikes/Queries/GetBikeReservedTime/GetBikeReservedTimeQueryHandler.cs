using Bike360.Application.Contracts.Persistence;
using Bike360.Application.Exceptions;
using Bike360.Application.Features.Bikes.Queries.GetBikeReservedDays;
using Bike360.Application.Features.Reservations.Services;
using Bike360.Domain;
using Bike360.Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Bike360.Application.Features.Bikes.Queries.GetBikeReservedTime;

public class GetBikeReservedTimeQueryHandler : IRequestHandler<GetBikeReservedTimeQuery, IEnumerable<DateRange>>
{
    private readonly IBikeRepository _bikeRepository;
    private readonly IReservationService _reservationService;
    private readonly ILogger<GetBikeReservedTimeQueryHandler> _logger;

    public GetBikeReservedTimeQueryHandler(
        IBikeRepository bikeRepository,
        IReservationService reservationService,
        ILogger<GetBikeReservedTimeQueryHandler> logger)
    {
        _bikeRepository = bikeRepository;
        _reservationService = reservationService;
        _logger = logger;
    }

    public async Task<IEnumerable<DateRange>> Handle(
        GetBikeReservedTimeQuery request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting reserved days for bike with ID = {BikeId} in period: {TimeStart} - {TimeEnd}", request.Id, request.TimeStart, request.TimeEnd);

        var validator = new GetBikeReservedTimeQueryValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (validationResult.Errors.Count != 0)
            throw new BadRequestException("Invalid request", validationResult);

        var bike = await _bikeRepository.GetByIdAsync(request.Id)
           ?? throw new NotFoundException(nameof(Bike), request.Id);

        var result = await _reservationService.GetBlockedDays(request.Id, request.TimeStart, request.TimeEnd);

        return result;
    }
}
