using AutoMapper;
using Bike360.Application.Contracts.Persistence;
using Bike360.Application.Exceptions;
using Bike360.Application.Features.Reservations.Services;
using Bike360.Domain;
using MediatR;

namespace Bike360.Application.Features.Reservations.Commands.CreateReservation;

public class CreateReservationCommandHandler : IRequestHandler<CreateReservationCommand, int>
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IBikeRepository _bikeRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;
    private readonly IReservationService _reservationService;

    public CreateReservationCommandHandler(
        IReservationRepository reservationRepository,
        IBikeRepository bikeRepository,
        ICustomerRepository customerRepository,
        IMapper mapper,
        IReservationService reservationService)
    {
        _reservationRepository = reservationRepository;
        _bikeRepository = bikeRepository;
        _customerRepository = customerRepository;
        _mapper = mapper;
        _reservationService = reservationService;
    }

    public async Task<int> Handle(
        CreateReservationCommand request,
        CancellationToken cancellationToken)
    {
        await ValidateCommand(request, cancellationToken);

        var customerData = await GetCustomerDetails(request.CustomerId);

        var reservationBikesEntities = await _bikeRepository.GetByIdsAsync(request.BikesIds);

        var reservationTimeEnd = request.DateTimeStart.AddDays(request.NumberOfDays);

        ValidateBikesAvailability(
            reservationBikesEntities,
            request.BikesIds,
            request.DateTimeStart,
            reservationTimeEnd);

        var reservationToCreate = CreateReservation(
            request,
            reservationBikesEntities,
            customerData,
            reservationTimeEnd);

        await _reservationRepository.CreateAsync(reservationToCreate);

        return reservationToCreate.Id;
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
                    ?? throw new BadRequestException($"Couldn't find customer with Id = {customerId}");
    }

    private Reservation CreateReservation(
        CreateReservationCommand request,
        IEnumerable<Bike> reservationBikesEntities,
        Customer customer,
        DateTime timeEnd)
    {
        var reservation = _mapper.Map<Reservation>(request);

        foreach (var bike in reservationBikesEntities)
        {
            reservation.Bikes.Add(bike);
        }

        reservation.DateTimeEnd = timeEnd;
        reservation.Customer = customer;

        return reservation;
    }

    private void ValidateBikesAvailability(
        IEnumerable<Bike> bikesEntities,
        IEnumerable<int> bikesIds,
        DateTime timeStart,
        DateTime timeEnd)
    {
        if (bikesEntities.Count() != bikesIds.Count())
            ThrowBikesInconsistencyError(bikesEntities, bikesIds);

        var bikesAvailability = _reservationService.CheckBikesAvailability(bikesIds, timeStart, timeEnd);

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

        throw new BadRequestException($"Invalid reservation data. Bikes with following IDs were not found: {idsString}");
    }
}
