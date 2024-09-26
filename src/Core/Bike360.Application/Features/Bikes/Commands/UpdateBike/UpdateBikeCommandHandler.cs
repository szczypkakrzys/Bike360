using AutoMapper;
using Bike360.Application.Contracts.Persistence;
using Bike360.Application.Exceptions;
using Bike360.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Bike360.Application.Features.Bikes.Commands.UpdateBike;

public class UpdateBikeCommandHandler : IRequestHandler<UpdateBikeCommand, Unit>
{
    private readonly IBikeRepository _bikeRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateBikeCommandHandler> _logger;

    public UpdateBikeCommandHandler(
        IBikeRepository bikeRepository,
        IMapper mapper,
        ILogger<UpdateBikeCommandHandler> logger)
    {
        _bikeRepository = bikeRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Unit> Handle(
        UpdateBikeCommand request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating bike with ID = {BikeId} with {@UpdatedBike}", request.Id, request);

        var validator = new UpdateBikeCommandValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (validationResult.Errors.Count != 0)
            throw new BadRequestException("Invalid bike data", validationResult);

        var bikeData = await _bikeRepository.GetByIdAsync(request.Id)
            ?? throw new NotFoundException(nameof(Bike), request.Id);

        _mapper.Map(request, bikeData);

        await _bikeRepository.UpdateAsync(bikeData);

        return Unit.Value;
    }
}
