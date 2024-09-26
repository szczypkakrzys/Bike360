using AutoMapper;
using Bike360.Application.Contracts.Persistence;
using Bike360.Application.Exceptions;
using Bike360.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Bike360.Application.Features.Bikes.Commands.CreateBike;

public class CreateBikeCommandHandler : IRequestHandler<CreateBikeCommand, int>
{
    private readonly IMapper _mapper;
    private readonly IBikeRepository _bikeRepository;
    private readonly ILogger<CreateBikeCommandHandler> _logger;

    public CreateBikeCommandHandler(
        IMapper mapper,
        IBikeRepository bikeRepository,
        ILogger<CreateBikeCommandHandler> logger)
    {
        _mapper = mapper;
        _bikeRepository = bikeRepository;
        _logger = logger;
    }

    public async Task<int> Handle(
        CreateBikeCommand request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating a new bike {@Bike}", request);

        var validator = new CreateBikeCommandValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (validationResult.Errors.Count != 0)
            throw new BadRequestException("Invalid Bike", validationResult);

        var bikeToCreate = _mapper.Map<Bike>(request);

        await _bikeRepository.CreateAsync(bikeToCreate);

        return bikeToCreate.Id;
    }
}
