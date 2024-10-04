using Bike360.UI.Contracts;
using Bike360.UI.Models;
using Bike360.UI.Models.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace Bike360.UI.Pages.Tours;

[Authorize(Policy = Policies.TravelAgency)]
public partial class Index
{
    [Inject]
    public NavigationManager NavManager { get; set; }

    [Inject]
    public ITourService Tour { get; set; }

    public List<ActivityVM> Tours { get; private set; }
    public string Message { get; set; } = string.Empty;
    public string SearchText = "";
    List<ActivityVM> FilteredTours => Tours.Where(
            tour => (tour.Name + tour.TimeStart + tour.TimeEnd).Contains(SearchText, StringComparison.CurrentCultureIgnoreCase)).ToList();
    protected override async Task OnInitializedAsync()
    {
        await LoadTours();
    }

    protected void CreateTour()
    {
        NavManager.NavigateTo("/travelagency/tours/create/");
    }

    protected void TourDetails(int id)
    {
        NavManager.NavigateTo($"/travelagency/tours/details/{id}");
    }

    public bool ParticipantsListCollapsed = true;

    Dictionary<int, bool> ParticipantsListCollapsedStates = [];

    protected async Task LoadTours()
    {
        Tours = await Tour.GetAllTours();

        foreach (var tour in Tours)
        {
            ParticipantsListCollapsedStates[tour.Id] = true;
        }
    }

    protected void ToggleParticipantsList(int tourId)
    {
        ParticipantsListCollapsedStates[tourId] = !ParticipantsListCollapsedStates[tourId];
    }

    string GetParticipantsActionText(int tourId)
    {
        return ParticipantsListCollapsedStates[tourId] ? "Poka¿ uczestników" : "Zwiñ listê";
    }
}