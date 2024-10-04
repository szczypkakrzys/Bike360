using Bike360.UI.Contracts;
using Bike360.UI.Models;
using Bike360.UI.Models.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace Bike360.UI.Pages.Courses;

[Authorize(Policy = Policies.DivingSchool)]
public partial class CourseParticipants
{
    [Parameter]
    public int CourseId { get; set; }
    [Parameter]
    public string CourseName { get; set; }

    [Inject]
    public NavigationManager NavManager { get; set; }

    [Inject]
    public ICourseService CourseService { get; set; }

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
        Participants = await CourseService.GetCourseParticipants(CourseId);
    }

    protected void CustomerCourses(int id)
    {
        NavManager.NavigateTo($"/divingschool/customers/{id}/courses");
    }
}