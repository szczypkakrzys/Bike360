﻿using AutoMapper;
using Bike360.Application.Contracts.Persistence;
using Bike360.Application.Exceptions;
using Bike360.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Bike360.Application.Features.Customers.Queries.GetCustomerReservations;

public class GetCustomerReservationsQueryHandler : IRequestHandler<GetCustomerReservationsQuery, IEnumerable<ReservationDto>>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IReservationRepository _reservationRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetCustomerReservationsQueryHandler> _logger;

    public GetCustomerReservationsQueryHandler(
        ICustomerRepository customerRepository,
        IReservationRepository reservationRepository,
        IMapper mapper,
        ILogger<GetCustomerReservationsQueryHandler> logger)
    {
        _customerRepository = customerRepository;
        _reservationRepository = reservationRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<ReservationDto>> Handle(
        GetCustomerReservationsQuery request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all reservations for customer with ID = {CustomerId}", request.CustomerId);

        var customerData = await _customerRepository.GetByIdAsync(request.CustomerId)
            ?? throw new NotFoundException(nameof(Customer), request.CustomerId);

        var customerReservations = await _reservationRepository.GetAllCustomerReservations(request.CustomerId);

        var result = _mapper.Map<IEnumerable<ReservationDto>>(customerReservations);

        return result;
    }
}
