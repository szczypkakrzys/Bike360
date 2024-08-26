using AutoMapper;
using Bike360.Application.Contracts.Persistence;
using MediatR;

namespace Bike360.Application.Features.Bikes.Queries.GetAllBikes;

public class GetAllBikesQueryHandler : IRequestHandler<GetAllBikesQuery, IEnumerable<BikeDto>>
{
    private readonly IBikeRepository _bikeRepository;
    private readonly IMapper _mapper;

    public GetAllBikesQueryHandler(
        IBikeRepository bikeRepository,
        IMapper mapper)
    {
        _bikeRepository = bikeRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<BikeDto>> Handle(
        GetAllBikesQuery request,
        CancellationToken cancellationToken)
    {
        var bikes = await _bikeRepository.GetAsync();

        var result = _mapper.Map<IEnumerable<BikeDto>>(bikes);

        return result;
    }
}
