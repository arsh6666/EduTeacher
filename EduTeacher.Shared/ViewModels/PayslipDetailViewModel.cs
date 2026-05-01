using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Rootfly.Mobile.Core.Common.Abstractions;
using Rootfly.Mobile.Core.Common.ViewModels;
using EduTeacher.Shared.Education.Dtos;
using EduTeacher.Shared.Education.Services;
using System.Collections.ObjectModel;

namespace EduTeacher.Shared.ViewModels;

public partial class PayslipDetailViewModel : BaseViewModel
{
    private readonly IPayrollApiService _payrollApi;
    private readonly ILocalizationService _l;

    [ObservableProperty] private string _slipId = string.Empty;
    [ObservableProperty] private SalarySlipDto? _payslip;
    [ObservableProperty] private string _periodLabel = string.Empty;

    public ObservableCollection<SalarySlipComponentDto> Earnings { get; } = [];
    public ObservableCollection<SalarySlipComponentDto> Deductions { get; } = [];

    public PayslipDetailViewModel(
        IPayrollApiService payrollApi,
        ILocalizationService localizationService)
    {
        _payrollApi = payrollApi;
        _l = localizationService;
        Title = "Payslip Detail";
    }

    public override async Task OnAppearingAsync()
    {
        await ExecuteBusyAsync(async () =>
        {
            Title = await _l.GetStringAsync("PayslipDetail");
            if (Guid.TryParse(SlipId, out var id))
            {
                var result = await _payrollApi.GetPayslipDetailAsync(id);
                if (result.IsSuccess && result.Data is not null)
                {
                    Payslip = result.Data;
                    PeriodLabel = $"{new DateTime(Payslip.Year, Payslip.Month, 1):MMMM yyyy}";

                    Earnings.Clear();
                    foreach (var e in Payslip.Earnings)
                        Earnings.Add(e);

                    Deductions.Clear();
                    foreach (var d in Payslip.Deductions)
                        Deductions.Add(d);
                }
            }
        });
    }
}
