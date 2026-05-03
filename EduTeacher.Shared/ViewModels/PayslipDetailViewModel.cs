using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Rootfly.Mobile.Core.Common.ViewModels;
using EduTeacher.Shared.Payroll.Dtos;
using EduTeacher.Shared.Payroll.Services;

namespace EduTeacher.Shared.ViewModels;

public partial class PayslipDetailViewModel : BaseViewModel
{
    private readonly IPayrollApiService _payrollApi;

    [ObservableProperty] private SalarySlipDto? _payslip;
    [ObservableProperty] private Guid _slipId;

    public PayslipDetailViewModel(IPayrollApiService payrollApi)
    {
        _payrollApi = payrollApi;
        Title = "Payslip";
    }

    [RelayCommand]
    private async Task LoadPayslip(Guid id)
    {
        SlipId = id;
        await ExecuteBusyAsync(async () =>
        {
            var result = await _payrollApi.GetPayslipDetailAsync(id);
            if (result.IsSuccess) Payslip = result.Data;
        });
    }
}
