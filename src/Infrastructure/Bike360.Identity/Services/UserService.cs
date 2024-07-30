using AutoMapper;
using Bike360.Application.Contracts.Identity;
using Bike360.Application.Exceptions;
using Bike360.Application.Features.SystemUsers.Commands.RegisterNewUser;
using Bike360.Application.Models.Identity;
using Bike360.Identity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Text;

namespace Bike360.Identity.Services;

public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _contextAccessor;

    public UserService(
        UserManager<ApplicationUser> userManager,
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor)
    {
        _userManager = userManager;
        _mapper = mapper;
        _contextAccessor = httpContextAccessor;
    }

    public string UserId { get => _contextAccessor.HttpContext?.User?.FindFirstValue("uid"); }

    //TODO
    //resolve issue with automapper mapping

    public async Task<List<Employee>> GetEmployees()
    {
        var employees = await _userManager.GetUsersInRoleAsync("Employee");

        return employees.Select(employee =>
            _mapper.Map<Employee>(employee))
            .ToList();
    }

    public async Task<Employee> GetEmployee(string userId)
    {
        var employee = await _userManager.FindByIdAsync(userId);

        return new Employee
        {
            Id = employee.Id,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Email = employee.Email,
        };
    }

    public async Task<IEnumerable<Employee>> GetEmployeesByRole(string roleName)
    {
        var employees = await _userManager.GetUsersInRoleAsync(roleName);

        return employees.Select(employee => new Employee
        {
            Id = employee.Id,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Email = employee.Email,
        });
    }

    public async Task DeleteEmployee(string userId)
    {
        var employee = await _userManager.FindByIdAsync(userId) ??
            throw new NotFoundException(nameof(Application), userId);

        await _userManager.DeleteAsync(employee);
    }

    public async Task ChangeEmail(string userId, string newEmail)
    {
        var employee = await _userManager.FindByIdAsync(userId) ??
            throw new NotFoundException(nameof(Application), userId);
        employee.Email = newEmail;
        employee.UserName = newEmail;
        await _userManager.UpdateAsync(employee);
    }

    public async Task<RegistrationResponse> Register(RegisterNewUserCommand request)
    {
        var user = new ApplicationUser
        {
            Email = request.EmailAddress,
            FirstName = request.FirstName,
            LastName = request.LastName,
            UserName = request.EmailAddress,
            EmailConfirmed = true
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, RoleName.GetRoleName(request.Role));
            return new RegistrationResponse() { UserId = user.Id };
        }
        else
        {
            var errorMessage = new StringBuilder();
            foreach (var error in result.Errors)
            {
                errorMessage.AppendFormat("- {0}\n", error.Description);
            }

            throw new BadRequestException($"{errorMessage}");
        }
    }

    //TODO
    //blocking / uncblocking accounts
    public async Task BlockEmloyee(string userId)
    {
        var employee = await _userManager.FindByIdAsync(userId);
        await _userManager.SetLockoutEnabledAsync(employee, true);
    }

    public async Task UnBlockEmloyee(string userId)
    {
        var employee = await _userManager.FindByIdAsync(userId);
        await _userManager.SetLockoutEnabledAsync(employee, false);
    }

}
