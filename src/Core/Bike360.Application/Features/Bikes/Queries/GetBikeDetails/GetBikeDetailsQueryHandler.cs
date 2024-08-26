using AutoMapper;
using Bike360.Application.Contracts.Persistence;
using Bike360.Application.Exceptions;
using Bike360.Domain;
using MediatR;

namespace Bike360.Application.Features.Bikes.Queries.GetBikeDetails;

public class GetBikeDetailsQueryHandler : IRequestHandler<GetBikeDetailsQuery, BikeDetailsDto>
{
    private readonly IBikeRepository _bikeRepository;
    private readonly IMapper _mapper;

    public GetBikeDetailsQueryHandler(
        IBikeRepository bikeRepository,
        IMapper mapper)
    {
        _bikeRepository = bikeRepository;
        _mapper = mapper;
    }

    public async Task<BikeDetailsDto> Handle(
        GetBikeDetailsQuery request,
        CancellationToken cancellationToken)
    {
        var bikeDetails = await _bikeRepository.GetByIdAsync(request.Id) ??
            throw new NotFoundException(nameof(Bike), request.Id);

        var data = _mapper.Map<BikeDetailsDto>(bikeDetails);

        return data;
    }
}
