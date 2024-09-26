using AutoMapper;
using Bike360.Application.Contracts.Persistence;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Bike360.Application.Features.Bikes.Queries.GetAllBikes;

public class GetAllBikesQueryHandler : IRequestHandler<GetAllBikesQuery, IEnumerable<BikeDto>>
{
    private readonly IBikeRepository _bikeRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetAllBikesQueryHandler> _logger;

    public GetAllBikesQueryHandler(
        IBikeRepository bikeRepository,
        IMapper mapper,
        ILogger<GetAllBikesQueryHandler> logger)
    {
        _bikeRepository = bikeRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<BikeDto>> Handle(
        GetAllBikesQuery request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all bikes");

        var bikes = await _bikeRepository.GetAsync();

        var result = _mapper.Map<IEnumerable<BikeDto>>(bikes);

        return result;
    }
}
