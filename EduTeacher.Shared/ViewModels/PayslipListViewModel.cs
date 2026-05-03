using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Rootfly.Mobile.Core.Common.ViewModels;
using Rootfly.Mobile.Core.Security.Interfaces;
using EduTeacher.Shared.Payroll.Dtos;
using EduTeacher.Shared.Payroll.Services;

namespace EduTeacher.Shared.ViewModels;

public partial class PayslipListViewModel : BaseViewModel
{
    private readonly IPayrollApiService _payrollApi;
    private readonly IAuthService _authService;

    [ObservableProperty] private ObservableCollection<SalarySlipDto> _payslips = new();

    public PayslipListViewModel(IPayrollApiService payrollApi, IAuthService authService)
    {
        _payrollApi = payrollApi;
        _authService = authService;
        Title = "My Payslips";
    }

    public override async Task OnAppearingAsync()
    {
        await ExecuteBusyAsync(async () =>
        {
            var user = await _authService.GetCurrentUserAsync();
            if (user is null) return;
            var result = await _payrollApi.GetPayslipsAsync(user.Id, maxResultCount: 24);
            if (result.IsSuccess && result.Data is not null)
                Payslips = new ObservableCollection<SalarySlipDto>(result.Data.Items);
        });
    }

    [RelayCommand]
    private Task ViewPayslip(Guid slipId) => Navigation.NavigateToAsync($"PayslipDetail?id={slipId}");
}
