using AntDesign;
using Bike360.UI.Contracts;
using Bike360.UI.Models;
using Bike360.UI.Models.TravelAgency;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace Bike360.UI.Pages.Tours;

[Authorize(Policy = Policies.TravelAgency)]
public partial class Edit
{
    [Inject]
    ITourService Tour { get; set; }

    [Inject]
    NavigationManager NavManager { get; set; }

    [Inject]
    IMessageService _message { get; set; }

    [Parameter]
    public int Id { get; set; }
    public string Message { get; private set; }

    TourDetailsVM Model = new();

    protected override async Task OnParametersSetAsync()
    {
        Model = await Tour.GetTourDetails(Id);
    }

    private async Task UpdateTour()
    {
        var response = await Tour.UpdateTour(Id, Model);
        if (response.IsSuccess)
        {
            NavManager.NavigateTo("/travelagency/tours/");
            _message.Success("Poprawnie zaaktualizowano dane wycieczki");
        }
        else
        {
            Message = response.Message;
            _message.Error("Nie uda³o siê edytowaæ danych wycieczki");
        }
    }
}