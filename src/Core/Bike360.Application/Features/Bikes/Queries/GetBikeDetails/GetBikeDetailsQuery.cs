using MediatR;

namespace Bike360.Application.Features.Bikes.Queries.GetBikeDetails;

public record GetBikeDetailsQuery(int Id) : IRequest<BikeDetailsDto>;
