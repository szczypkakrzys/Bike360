using Bike360.Application.Contracts.Persistence;
using Bike360.Application.Exceptions;
using Bike360.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Bike360.Application.Features.Bikes.Commands.DeleteBike;

public class DeleteBikeCommandHandler : IRequestHandler<DeleteBikeCommand, Unit>
{
    private readonly IBikeRepository _bikeRepository;
    private readonly ILogger<DeleteBikeCommandHandler> _logger;

    public DeleteBikeCommandHandler(
        IBikeRepository bikeRepository,
        ILogger<DeleteBikeCommandHandler> logger)
    {
        _bikeRepository = bikeRepository;
        _logger = logger;
    }

    public async Task<Unit> Handle(
        DeleteBikeCommand request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting bike with ID = {BikeId}", request.Id);

        var bikeToDelete = await _bikeRepository.GetByIdAsync(request.Id) ??
                            throw new NotFoundException(nameof(Bike), request.Id);

        await _bikeRepository.DeleteAsync(bikeToDelete);

        return Unit.Value;
    }
}
