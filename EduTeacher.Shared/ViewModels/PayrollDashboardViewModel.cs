using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Rootfly.Mobile.Core.Common.ViewModels;
using Rootfly.Mobile.Core.Security.Interfaces;
using EduTeacher.Shared.Payroll.Dtos;
using EduTeacher.Shared.Payroll.Services;

namespace EduTeacher.Shared.ViewModels;

public partial class PayrollDashboardViewModel : BaseViewModel
{
    private readonly IPayrollApiService _payrollApi;
    private readonly IAuthService _authService;

    [ObservableProperty] private SalaryStructureAssignmentDto? _salaryStructure;
    [ObservableProperty] private SalarySlipDto? _latestPayslip;
    [ObservableProperty] private decimal _netPay;
    [ObservableProperty] private decimal _ctc;
    [ObservableProperty] private decimal _grossPay;
    [ObservableProperty] private string _payPeriod = string.Empty;
    [ObservableProperty] private Guid _employeeId;

    public PayrollDashboardViewModel(IPayrollApiService payrollApi, IAuthService authService)
    {
        _payrollApi = payrollApi;
        _authService = authService;
        Title = "Payroll";
    }

    public override async Task OnAppearingAsync()
    {
        await ExecuteBusyAsync(async () =>
        {
            var user = await _authService.GetCurrentUserAsync();
            if (user is null) return;
            // TODO: resolve employeeId from current user via HR employee lookup
            // For now, use user.Id as placeholder — backend should link user to employee
            EmployeeId = user.Id;

            var structureResult = await _payrollApi.GetSalaryStructureAsync(EmployeeId);
            if (structureResult.IsSuccess && structureResult.Data?.Items.Count > 0)
            {
                SalaryStructure = structureResult.Data.Items[0];
                Ctc = SalaryStructure.CTC;
            }

            var payslipsResult = await _payrollApi.GetPayslipsAsync(EmployeeId, maxResultCount: 1);
            if (payslipsResult.IsSuccess && payslipsResult.Data?.Items.Count > 0)
            {
                LatestPayslip = payslipsResult.Data.Items[0];
                NetPay = LatestPayslip.NetPay;
                GrossPay = LatestPayslip.GrossPay;
                PayPeriod = $"{LatestPayslip.StartDate:MMM yyyy}";
            }
        });
    }

    [RelayCommand]
    private Task NavigateToPayslips() => Navigation.NavigateToAsync("PayslipList");

    [RelayCommand]
    private Task NavigateToTaxDeclaration() => Navigation.NavigateToAsync("TaxDeclaration");

    [RelayCommand]
    private Task NavigateToReimbursements() => Navigation.NavigateToAsync("Reimbursements");

    [RelayCommand]
    private Task NavigateToSalaryAdvance() => Navigation.NavigateToAsync("SalaryAdvance");

    [RelayCommand]
    private Task NavigateToOvertime() => Navigation.NavigateToAsync("Overtime");
}
