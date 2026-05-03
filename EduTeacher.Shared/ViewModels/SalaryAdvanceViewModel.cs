using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Rootfly.Mobile.Core.Common.ViewModels;
using Rootfly.Mobile.Core.Security.Interfaces;
using EduTeacher.Shared.Payroll.Dtos;
using EduTeacher.Shared.Payroll.Services;

namespace EduTeacher.Shared.ViewModels;

public partial class SalaryAdvanceViewModel : BaseViewModel
{
    private readonly IPayrollApiService _payrollApi;
    private readonly IAuthService _authService;

    [ObservableProperty] private ObservableCollection<SalaryAdvanceDto> _advances = new();
    [ObservableProperty] private decimal _requestAmount;
    [ObservableProperty] private string _reason = string.Empty;
    [ObservableProperty] private int _repaymentMonths = 3;
    [ObservableProperty] private bool _showRequestForm;

    public SalaryAdvanceViewModel(IPayrollApiService payrollApi, IAuthService authService)
    {
        _payrollApi = payrollApi;
        _authService = authService;
        Title = "Salary Advance";
    }

    public override async Task OnAppearingAsync()
    {
        await ExecuteBusyAsync(async () =>
        {
            var user = await _authService.GetCurrentUserAsync();
            if (user is null) return;
            var result = await _payrollApi.GetSalaryAdvancesAsync(user.Id);
            if (result.IsSuccess && result.Data is not null)
                Advances = new ObservableCollection<SalaryAdvanceDto>(result.Data.Items);
        });
    }

    [RelayCommand]
    private async Task RequestAdvance()
    {
        if (RequestAmount <= 0) { ErrorMessage = "Enter a valid amount"; return; }
        await ExecuteBusyAsync(async () =>
        {
            var user = await _authService.GetCurrentUserAsync();
            if (user is null) return;
            var input = new CreateSalaryAdvanceDto
            {
                EmployeeId = user.Id,
                Amount = RequestAmount,
                Reason = Reason,
                RepaymentMonths = RepaymentMonths
            };
            var result = await _payrollApi.RequestSalaryAdvanceAsync(input);
            if (result.IsSuccess)
            {
                await Dialog.ShowToastAsync("Advance requested successfully");
                ShowRequestForm = false;
                RequestAmount = 0; Reason = string.Empty; RepaymentMonths = 3;
                await OnAppearingAsync();
            }
            else ErrorMessage = result.Error ?? "Request failed";
        });
    }

    [RelayCommand]
    private void ToggleRequestForm() => ShowRequestForm = !ShowRequestForm;
}
