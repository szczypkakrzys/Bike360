using AutoMapper;
using Bike360.Application.Contracts.Persistence;
using Bike360.Application.Exceptions;
using Bike360.Domain;
using MediatR;

namespace Bike360.Application.Features.Bikes.Commands.UpdateBike;

public class UpdateBikeCommandHandler : IRequestHandler<UpdateBikeCommand, Unit>
{
    private readonly IBikeRepository _bikeRepository;
    private readonly IMapper _mapper;

    public UpdateBikeCommandHandler(
        IBikeRepository bikeRepository,
        IMapper mapper)
    {
        _bikeRepository = bikeRepository;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(
        UpdateBikeCommand request,
        CancellationToken cancellationToken)
    {
        var validator = new UpdateBikeCommandValidator(_bikeRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (validationResult.Errors.Count != 0)
            throw new BadRequestException("Invalid bike data", validationResult);

        var bikeToUpdate = _mapper.Map<Bike>(request);

        await _bikeRepository.UpdateAsync(bikeToUpdate);

        return Unit.Value;
    }
}
