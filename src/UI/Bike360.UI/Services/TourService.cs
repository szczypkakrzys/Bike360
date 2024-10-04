using AutoMapper;
using Blazored.LocalStorage;
using Bike360.UI.Contracts;
using Bike360.UI.Models.Shared;
using Bike360.UI.Models.TravelAgency;
using Bike360.UI.Services.Base;

namespace Bike360.UI.Services;

public class TourService : BaseHttpService, ITourService
{
    private readonly IMapper _mapper;

    public TourService(
        IClient client,
        ILocalStorageService localStorageService,
        IMapper mapper) : base(client, localStorageService)
    {
        _mapper = mapper;
    }

    public async Task<Response<Guid>> CreateTour(TourDetailsVM tour)
    {
        try
        {
            await AddBearerToken();
            var createTourCommand = _mapper.Map<CreateTourCommand>(tour);
            await _client.ToursPOSTAsync(createTourCommand);
            return new Response<Guid>() { IsSuccess = true };
        }
        catch (ApiException ex)
        {
            return ConvertApiExceptions<Guid>(ex);
        }
    }

    public async Task<Response<Guid>> DeleteTour(int id)
    {
        try
        {
            await AddBearerToken();
            await _client.ToursDELETEAsync(id);
            return new Response<Guid>() { IsSuccess = true };
        }
        catch (ApiException ex)
        {
            return ConvertApiExceptions<Guid>(ex);
        }
    }

    public async Task<List<ActivityVM>> GetAllTours()
    {
        await AddBearerToken();
        var tours = await _client.ToursAllAsync();
        return _mapper.Map<List<ActivityVM>>(tours);
    }

    public async Task<TourDetailsVM> GetTourDetails(int id)
    {
        await AddBearerToken();
        var tour = await _client.ToursGETAsync(id);
        return _mapper.Map<TourDetailsVM>(tour);
    }

    public async Task<Response<Guid>> UpdateTour(int id, TourDetailsVM tour)
    {
        try
        {
            await AddBearerToken();
            var updateTourCommand = _mapper.Map<UpdateTourCommand>(tour);
            await _client.ToursPUTAsync(id.ToString(), updateTourCommand);
            return new Response<Guid>() { IsSuccess = true };
        }
        catch (ApiException ex)
        {
            return ConvertApiExceptions<Guid>(ex);
        }
    }

    public async Task<List<ActivityParticipantVM>> GetTourParticipants(int tourId)
    {
        await AddBearerToken();
        var participants = await _client.Participants2Async(tourId);
        return _mapper.Map<List<ActivityParticipantVM>>(participants);
    }
}
