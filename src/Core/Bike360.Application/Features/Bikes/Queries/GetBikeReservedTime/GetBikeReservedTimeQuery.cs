using Bike360.Domain.Models;
using MediatR;

namespace Bike360.Application.Features.Bikes.Queries.GetBikeReservedDays;

public record GetBikeReservedTimeQuery(int Id, DateTime TimeStart, DateTime TimeEnd) : IRequest<IEnumerable<DateRange>>;