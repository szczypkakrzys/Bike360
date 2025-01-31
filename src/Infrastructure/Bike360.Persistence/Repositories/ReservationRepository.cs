﻿using Bike360.Application.Contracts.Persistence;
using Bike360.Domain;
using Bike360.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace Bike360.Persistence.Repositories;

public class ReservationRepository : GenericRepository<Reservation>, IReservationRepository
{
    public ReservationRepository(Bike360DatabaseContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Reservation>> GetAllBikeReservationsInGivenPeriod(
        int bikeId,
        DateTime periodStart,
        DateTime periodEnd)
    {
        return await _context.Reservations
            .Where(reservation => reservation.Bikes.Any(bike => bike.Id == bikeId) &&
                                  reservation.DateTimeStartInUtc <= periodEnd &&
                                  reservation.DateTimeEndInUtc >= periodStart)
            .ToListAsync();
    }

    public async Task<IEnumerable<Reservation>> GetAllCustomerReservations(int customerId)
    {
        return await _context.Reservations
            .Where(reservation => reservation.CustomerId == customerId)
            .ToListAsync();
    }

    public async Task<Reservation> GetReservationWithDetails(int reservationId)
    {
        return await _context.Reservations
            .Include(reservation => reservation.Customer)
            .Include(reservation => reservation.Bikes)
            .FirstOrDefaultAsync(reservation => reservation.Id == reservationId);
    }
}
