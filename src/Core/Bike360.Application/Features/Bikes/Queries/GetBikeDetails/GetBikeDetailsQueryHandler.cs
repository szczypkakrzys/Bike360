using AutoMapper;
using Bike360.Application.Contracts.Persistence;
using Bike360.Application.Exceptions;
using Bike360.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Bike360.Application.Features.Bikes.Queries.GetBikeDetails;

public class GetBikeDetailsQueryHandler : IRequestHandler<GetBikeDetailsQuery, BikeDetailsDto>
{
    private readonly IBikeRepository _bikeRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetBikeDetailsQueryHandler> _logger;

    public GetBikeDetailsQueryHandler(
        IBikeRepository bikeRepository,
        IMapper mapper,
        ILogger<GetBikeDetailsQueryHandler> logger)
    {
        _bikeRepository = bikeRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<BikeDetailsDto> Handle(
        GetBikeDetailsQuery request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting bike with ID = {BikeId}", request.Id);

        var bikeDetails = await _bikeRepository.GetByIdAsync(request.Id) ??
            throw new NotFoundException(nameof(Bike), request.Id);

        var data = _mapper.Map<BikeDetailsDto>(bikeDetails);

        return data;
    }
}
