using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Rootfly.Mobile.Core.Common.Abstractions;
using Rootfly.Mobile.Core.Common.ViewModels;
using EduTeacher.Shared.Education.Dtos;
using EduTeacher.Shared.Education.Services;

namespace EduTeacher.Shared.ViewModels;

public partial class PayrollDashboardViewModel : BaseViewModel
{
    private readonly IPayrollApiService _payrollApi;
    private readonly ITeacherEducationApiService _educationApi;
    private readonly ILocalizationService _l;

    [ObservableProperty] private SalaryStructureAssignmentDto? _salaryStructure;
    [ObservableProperty] private SalarySlipDto? _latestPayslip;
    [ObservableProperty] private InstructorDto? _instructor;
    [ObservableProperty] private decimal _netPay;
    [ObservableProperty] private decimal _ctc;

    public PayrollDashboardViewModel(
        IPayrollApiService payrollApi,
        ITeacherEducationApiService educationApi,
        ILocalizationService localizationService)
    {
        _payrollApi = payrollApi;
        _educationApi = educationApi;
        _l = localizationService;
        Title = "Payroll";
    }

    public override async Task OnAppearingAsync()
    {
        await ExecuteBusyAsync(async () =>
        {
            Title = await _l.GetStringAsync("Payroll");

            var instructorResult = await _educationApi.GetCurrentInstructorAsync();
            if (!instructorResult.IsSuccess || instructorResult.Data?.EmployeeId is null) return;

            Instructor = instructorResult.Data;
            var employeeId = Instructor.EmployeeId!.Value;

            var structureResult = await _payrollApi.GetSalaryStructureAsync(employeeId);
            if (structureResult.IsSuccess && structureResult.Data is not null)
            {
                SalaryStructure = structureResult.Data;
                Ctc = SalaryStructure.Ctc;
            }

            var payslipsResult = await _payrollApi.GetPayslipsAsync(employeeId, 1);
            if (payslipsResult.IsSuccess && payslipsResult.Data?.Items.Count > 0)
            {
                LatestPayslip = payslipsResult.Data.Items[0];
                NetPay = LatestPayslip.NetPay;
            }
        });
    }

    [RelayCommand]
    private async Task NavigateToPayslipsAsync()
        => await Navigation.NavigateToAsync("PayslipList");

    [RelayCommand]
    private async Task NavigateToTaxAsync()
        => await Navigation.NavigateToAsync("TaxDeclaration");

    [RelayCommand]
    private async Task NavigateToReimbursementsAsync()
        => await Navigation.NavigateToAsync("Reimbursements");
}
