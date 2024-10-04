using AutoMapper;
using Bike360.Application.Contracts.Persistence;
using Bike360.Application.Exceptions;
using Bike360.Application.Features.Reservations.Constants;
using Bike360.Application.Features.Reservations.Services;
using Bike360.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Bike360.Application.Features.Reservations.Commands.CreateReservation;

public class CreateReservationCommandHandler : IRequestHandler<CreateReservationCommand, int>
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IBikeRepository _bikeRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;
    private readonly IReservationService _reservationService;
    private readonly ILogger<CreateReservationCommandHandler> _logger;

    public CreateReservationCommandHandler(
        IReservationRepository reservationRepository,
        IBikeRepository bikeRepository,
        ICustomerRepository customerRepository,
        IMapper mapper,
        IReservationService reservationService,
        ILogger<CreateReservationCommandHandler> logger)
    {
        _reservationRepository = reservationRepository;
        _bikeRepository = bikeRepository;
        _customerRepository = customerRepository;
        _mapper = mapper;
        _reservationService = reservationService;
        _logger = logger;
    }

    public async Task<int> Handle(
        CreateReservationCommand request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating a new reservation {@Reservation}", request);

        await ValidateCommand(request, cancellationToken);

        var customerData = await GetCustomerDetails(request.CustomerId);

        var reservationBikesEntities = await _bikeRepository.GetByIdsAsync(request.BikesIds);

        var reservationTimeEnd = CalculateReservationTimeEnd(request.DateTimeStartInUtc, request.NumberOfDays);

        await ValidateBikesAvailability(
            reservationBikesEntities,
            request.BikesIds,
            request.DateTimeStartInUtc,
            reservationTimeEnd);

        var reservationToCreate = PrepareReservationEntity(
            request,
            reservationBikesEntities,
            customerData);

        await _reservationRepository.CreateAsync(reservationToCreate);

        return reservationToCreate.Id;
    }

    private static DateTime CalculateReservationTimeEnd(DateTime timeStart, int numberOfDays)
    {
        return timeStart.AddDays(numberOfDays);
    }

    private async Task ValidateCommand(
        CreateReservationCommand request,
        CancellationToken cancellationToken)
    {
        var validator = new CreateReservationCommandValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (validationResult.Errors.Count != 0)
            throw new BadRequestException("Invalid Reservation", validationResult);
    }

    private async Task<Customer> GetCustomerDetails(int customerId)
    {
        return await _customerRepository.GetByIdAsync(customerId)
                    ?? throw new NotFoundException(nameof(Customer), customerId);
    }

    private Reservation PrepareReservationEntity(
        CreateReservationCommand request,
        IEnumerable<Bike> reservationBikesEntities,
        Customer customer)
    {
        var reservation = _mapper.Map<Reservation>(request);

        foreach (var bike in reservationBikesEntities)
        {
            reservation.Bikes.Add(bike);
        }

        reservation.DateTimeEndInUtc = CalculateReservationTimeEnd(request.DateTimeStartInUtc, request.NumberOfDays);
        reservation.Customer = customer;
        reservation.Cost = _reservationService.CalculateReservationCost(reservationBikesEntities, request.NumberOfDays);
        reservation.Status = ReservationStatus.Pending;

        return reservation;
    }

    private async Task ValidateBikesAvailability(
        IEnumerable<Bike> bikesEntities,
        IEnumerable<int> bikesIds,
        DateTime timeStart,
        DateTime timeEnd)
    {
        if (bikesEntities.Count() != bikesIds.Count())
            ThrowBikesInconsistencyError(bikesEntities, bikesIds);

        var bikesAvailability = await _reservationService.CheckBikesAvailability(bikesIds, timeStart, timeEnd);

        if (!bikesAvailability.AreAvailable)
            throw new BadRequestException(bikesAvailability.ErrorMessage);
    }

    private static void ThrowBikesInconsistencyError(
        IEnumerable<Bike> bikesEntities,
        IEnumerable<int> requestBikesIds)
    {
        var foundIds = new HashSet<int>(bikesEntities.Select(b => b.Id));

        var inconsistencies = requestBikesIds.Where(id => !foundIds.Contains(id)).ToList();

        var idsString = string.Join(", ", inconsistencies);

        throw new NotFoundException($"Invalid reservation data. Bikes with following IDs were not found: {idsString}");
    }
}
