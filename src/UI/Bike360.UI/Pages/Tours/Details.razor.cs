using AntDesign;
using Bike360.UI.Contracts;
using Bike360.UI.Models;
using Bike360.UI.Models.TravelAgency;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace Bike360.UI.Pages.Tours;

[Authorize(Policy = Policies.TravelAgency)]
public partial class Details
{
    [Inject]
    public NavigationManager NavManager { get; set; }

    [Inject]
    ITourService Tour { get; set; }

    [Inject]
    IMessageService _message { get; set; }

    [Parameter]
    public int Id { get; set; }

    TourDetailsVM tour = new();
    public string Message { get; set; } = string.Empty;

    protected override async Task OnParametersSetAsync()
    {
        tour = await Tour.GetTourDetails(Id);
    }
    protected void EditTour(int id)
    {
        NavManager.NavigateTo($"/travelagency/tours/edit/{id}");
    }

    protected async void DeleteTour(int id)
    {
        var response = await Tour.DeleteTour(id);
        if (response.IsSuccess)
        {
            NavManager.NavigateTo("/travelagency/tours/");
            _message.Info("Poprawnie usuniêto wycieczkê");
        }
        else
        {
            Message = response.Message;
            _message.Error("Nie uda³o siê usun¹æ wycieczki");
        }
    }
}