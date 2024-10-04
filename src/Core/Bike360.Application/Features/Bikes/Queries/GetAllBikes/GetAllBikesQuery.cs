using MediatR;

namespace Bike360.Application.Features.Bikes.Queries.GetAllBikes;

public record GetAllBikesQuery : IRequest<IEnumerable<BikeDto>>;
