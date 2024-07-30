using Bike360.UI.Models.Shared;
using Bike360.UI.Models.TravelAgency;
using Bike360.UI.Services.Base;

namespace Bike360.UI.Contracts;

public interface ITourService
{
    Task<List<ActivityVM>> GetAllTours();
    Task<TourDetailsVM> GetTourDetails(int id);
    Task<Response<Guid>> CreateTour(TourDetailsVM tour);
    Task<Response<Guid>> UpdateTour(int id, TourDetailsVM tour);
    Task<Response<Guid>> DeleteTour(int id);
    Task<List<ActivityParticipantVM>> GetTourParticipants(int tourId);
}
