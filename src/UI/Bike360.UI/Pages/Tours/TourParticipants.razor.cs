using Bike360.UI.Contracts;
using Bike360.UI.Models;
using Bike360.UI.Models.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace Bike360.UI.Pages.Tours;

[Authorize(Policy = Policies.TravelAgency)]
public partial class TourParticipants
{
    [Parameter]
    public int TourId { get; set; }
    [Parameter]
    public string TourName { get; set; }

    [Inject]
    public NavigationManager NavManager { get; set; }

    [Inject]
    public ITourService TourService { get; set; }

    public List<ActivityParticipantVM> Participants { get; private set; }
    public string Message { get; set; } = string.Empty;
    public string SearchText = "";
    List<ActivityParticipantVM> FilteredCustomers => Participants.Where(
            customer => (customer.FirstName + " " + customer.LastName).Contains(SearchText, StringComparison.CurrentCultureIgnoreCase)).ToList();
    protected override async Task OnInitializedAsync()
    {
        await LoadParticipants();
    }

    protected async Task LoadParticipants()
    {
        Participants = await TourService.GetTourParticipants(TourId);
    }

    protected void CustomerTours(int id)
    {
        NavManager.NavigateTo($"/travelagency/customers/{id}/tours");
    }
}