using AntDesign;
using Bike360.UI.Contracts;
using Bike360.UI.Models;
using Bike360.UI.Models.Shared;
using Bike360.UI.Models.TravelAgency;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace Bike360.UI.Pages.TravelAgencyCustomers;

[Authorize(Policy = Policies.TravelAgency)]
public partial class CustomerTours
{
    [Inject]
    public NavigationManager NavManager { get; set; }

    [Inject]
    ITravelAgencyCustomerService CustomerService { get; set; }

    [Inject]
    IMessageService _message { get; set; }

    [Parameter]
    public int Id { get; set; }

    TravelAgencyCustomerDetailsVM customer = new();
    public List<CustomerActivityVM> customerTours { get; private set; }
    CustomerActivityDetailsVM? customerTourDetails { get; set; }
    public string Message { get; set; } = string.Empty;
    public string SearchText = "";
    public bool ToursListCollapsed = true;
    string AddToTourActionText { get => ToursListCollapsed ? "Dodaj do wycieczki" : "Zwiñ listê"; }


    bool customerTourDetailsModalVisible = false;
    int modalTourId;
    string modalTourName = "";

    private string CustomerTourDetailsModalName() =>
        "Szczegó³y wycieczki " + modalTourName;

    double newPaymentValue;
    bool paymentModalVisible = false;

    List<CustomerActivityVM> FilteredTours => customerTours.Where(
           tour => (tour.Name + tour.TimeStart + tour.TimeEnd).Contains(SearchText, StringComparison.CurrentCultureIgnoreCase)).ToList();

    protected override async Task OnParametersSetAsync()
    {
        customer = await CustomerService.GetCustomerDetails(Id);
        customerTours = await CustomerService.GetCustomerTours(Id);
    }

    protected async void ShowCustomerTourDetails(int customerId, int tourId)
    {
        customerTourDetails = await CustomerService.CustomerTourDetails(customerId, tourId);
    }

    protected async Task RemoveFromTour(int customerId, int tourId)
    {
        var response = await CustomerService.RemoveCustomerFromTour(customerId, tourId);
        if (response.IsSuccess)
        {
            customerTours = await CustomerService.GetCustomerTours(Id);
            _message.Info("Usuniêto klienta z wycieczki");
        }
        else
        {
            _message.Error("Nie uda³o siê usun¹æ klienta z wycieczki");
            Message = response.Message;
        }
    }
    [Inject]
    public ITourService Tour { get; set; }
    public List<ActivityVM> Tours { get; private set; }
    public string AllToursSearchText = "";
    List<ActivityVM> FilteredAllToursList => Tours.Where(
            tour => (tour.Name + tour.TimeStart + tour.TimeEnd).Contains(AllToursSearchText, StringComparison.CurrentCultureIgnoreCase)).ToList();
    bool confirmModalVisible = false;
    string choosenTourName = "";
    int assignTourId;
    protected async Task ToggleToursList()
    {
        ToursListCollapsed = !ToursListCollapsed;
        Tours = await Tour.GetAllTours();
    }
    private async Task AssignCustomerToTourHandler(int tourId, int customerId)
    {
        var response = await CustomerService.AssignCustomerToTour(customerId, tourId);
        if (response.IsSuccess)
        {
            _message.Success("Przypisano klienta do wycieczki");
            customerTours = await CustomerService.GetCustomerTours(Id);
        }
        else
        {
            Message = response.Message;
            _message.Error("Nie uda³o siê przypisaæ klienta do wycieczki");
        }
    }
    protected void TourDetails(int id)
    {
        NavManager.NavigateTo($"/travelagency/tours/details/{id}");
    }
    protected void ShowCustomerTourDetailsModal(int customerId, int tourId)
    {
        ShowCustomerTourDetails(customerId, tourId);
        customerTourDetailsModalVisible = true;
    }

    protected void ShowPaymentModal()
    {
        paymentModalVisible = true;
    }

    protected async Task AddPayment(int customerId, int tourId, double paymentAmount)
    {
        var response = await CustomerService.UpdateCustomerPayment(
            customerId,
            tourId,
            paymentAmount);

        if (response.IsSuccess)
        {
            newPaymentValue = 0;
            customerTourDetails = await CustomerService.CustomerTourDetails(customerId, tourId);
            _message.Success("Dodano now¹ p³atnoœæ");
        }
    }

    protected void ClearCustomerTourDetails()
    {
        customerTourDetails = null;
    }
}