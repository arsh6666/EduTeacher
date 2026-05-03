using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Rootfly.Mobile.Core.Common.ViewModels;
using Rootfly.Mobile.Core.Security.Interfaces;
using EduTeacher.Shared.Payroll.Dtos;
using EduTeacher.Shared.Payroll.Services;

namespace EduTeacher.Shared.ViewModels;

public partial class OvertimeViewModel : BaseViewModel
{
    private readonly IPayrollApiService _payrollApi;
    private readonly IAuthService _authService;

    [ObservableProperty] private ObservableCollection<OvertimeEntryDto> _entries = new();
    [ObservableProperty] private ObservableCollection<OvertimeRuleDto> _rules = new();
    [ObservableProperty] private DateTime _selectedDate = DateTime.Today;
    [ObservableProperty] private decimal _hours;
    [ObservableProperty] private Guid _selectedRuleId;
    [ObservableProperty] private string _notes = string.Empty;
    [ObservableProperty] private bool _showLogForm;
    [ObservableProperty] private decimal _totalHoursThisMonth;

    public OvertimeViewModel(IPayrollApiService payrollApi, IAuthService authService)
    {
        _payrollApi = payrollApi;
        _authService = authService;
        Title = "Overtime";
    }

    public override async Task OnAppearingAsync()
    {
        await ExecuteBusyAsync(async () =>
        {
            var user = await _authService.GetCurrentUserAsync();
            if (user is null) return;

            var result = await _payrollApi.GetOvertimeEntriesAsync(user.Id);
            if (result.IsSuccess && result.Data is not null)
            {
                Entries = new ObservableCollection<OvertimeEntryDto>(result.Data.Items);
                TotalHoursThisMonth = result.Data.Items
                    .Where(e => e.Date.Month == DateTime.Today.Month && e.Date.Year == DateTime.Today.Year)
                    .Sum(e => e.Hours);
            }

            var rules = await _payrollApi.GetOvertimeRulesAsync();
            if (rules.IsSuccess && rules.Data is not null)
            {
                Rules = new ObservableCollection<OvertimeRuleDto>(rules.Data.Items);
                if (Rules.Count > 0 && SelectedRuleId == Guid.Empty)
                    SelectedRuleId = Rules[0].Id;
            }
        });
    }

    [RelayCommand]
    private async Task LogOvertime()
    {
        if (Hours <= 0) { ErrorMessage = "Enter valid hours"; return; }
        await ExecuteBusyAsync(async () =>
        {
            var user = await _authService.GetCurrentUserAsync();
            if (user is null) return;
            var input = new CreateOvertimeEntryDto
            {
                EmployeeId = user.Id,
                Date = SelectedDate,
                Hours = Hours,
                OvertimeRuleId = SelectedRuleId,
                Notes = Notes
            };
            var result = await _payrollApi.LogOvertimeAsync(input);
            if (result.IsSuccess)
            {
                await Dialog.ShowToastAsync("Overtime logged");
                ShowLogForm = false; Hours = 0; Notes = string.Empty;
                await OnAppearingAsync();
            }
            else ErrorMessage = result.Error ?? "Failed to log overtime";
        });
    }

    [RelayCommand]
    private void ToggleLogForm() => ShowLogForm = !ShowLogForm;
}
