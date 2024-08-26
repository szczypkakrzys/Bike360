using AutoMapper;
using Bike360.Application.Contracts.Persistence;
using Bike360.Application.Exceptions;
using Bike360.Domain;
using MediatR;

namespace Bike360.Application.Features.Bikes.Commands.CreateBike;

public class CreateBikeCommandHandler : IRequestHandler<CreateBikeCommand, int>
{
    private readonly IMapper _mapper;
    private readonly IBikeRepository _bikeRepository;

    public CreateBikeCommandHandler(
        IMapper mapper,
        IBikeRepository bikeRepository)
    {
        _mapper = mapper;
        _bikeRepository = bikeRepository;
    }

    public async Task<int> Handle(
        CreateBikeCommand request,
        CancellationToken cancellationToken)
    {
        //TODO
        //add checking for customer exists in tests :)

        var validator = new CreateBikeCommandValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (validationResult.Errors.Count != 0)
            throw new BadRequestException("Invalid Bike", validationResult);

        var bikeToCreate = _mapper.Map<Bike>(request);

        await _bikeRepository.CreateAsync(bikeToCreate);

        return bikeToCreate.Id;
    }
}
