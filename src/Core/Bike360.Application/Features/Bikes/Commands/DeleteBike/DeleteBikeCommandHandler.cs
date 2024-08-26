using Bike360.Application.Contracts.Persistence;
using Bike360.Application.Exceptions;
using Bike360.Domain;
using MediatR;

namespace Bike360.Application.Features.Bikes.Commands.DeleteBike;

public class DeleteBikeCommandHandler : IRequestHandler<DeleteBikeCommand, Unit>
{
    private readonly IBikeRepository _bikeRepository;

    public DeleteBikeCommandHandler(IBikeRepository bikeRepository)
    {
        _bikeRepository = bikeRepository;
    }

    public async Task<Unit> Handle(
        DeleteBikeCommand request,
        CancellationToken cancellationToken)
    {
        var bikeToDelete = await _bikeRepository.GetByIdAsync(request.Id) ??
                            throw new NotFoundException(nameof(Bike), request.Id);

        await _bikeRepository.DeleteAsync(bikeToDelete);

        return Unit.Value;
    }
}
