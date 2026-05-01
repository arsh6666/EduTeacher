using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Rootfly.Mobile.Core.Common.Abstractions;
using Rootfly.Mobile.Core.Common.ViewModels;
using EduTeacher.Shared.Education.Dtos;
using EduTeacher.Shared.Education.Services;
using System.Collections.ObjectModel;

namespace EduTeacher.Shared.ViewModels;

public partial class PayslipListViewModel : BaseViewModel
{
    private readonly IPayrollApiService _payrollApi;
    private readonly ITeacherEducationApiService _educationApi;
    private readonly ILocalizationService _l;

    public ObservableCollection<SalarySlipDto> Payslips { get; } = [];

    public PayslipListViewModel(
        IPayrollApiService payrollApi,
        ITeacherEducationApiService educationApi,
        ILocalizationService localizationService)
    {
        _payrollApi = payrollApi;
        _educationApi = educationApi;
        _l = localizationService;
        Title = "Payslips";
    }

    public override async Task OnAppearingAsync()
    {
        await ExecuteBusyAsync(async () =>
        {
            Title = await _l.GetStringAsync("Payslips");

            var instructorResult = await _educationApi.GetCurrentInstructorAsync();
            if (!instructorResult.IsSuccess || instructorResult.Data?.EmployeeId is null) return;

            var employeeId = instructorResult.Data.EmployeeId.Value;
            var result = await _payrollApi.GetPayslipsAsync(employeeId, 12);
            if (result.IsSuccess && result.Data is not null)
            {
                Payslips.Clear();
                foreach (var slip in result.Data.Items)
                    Payslips.Add(slip);
            }
        });
    }

    [RelayCommand]
    private async Task ViewDetailAsync(SalarySlipDto slip)
        => await Navigation.NavigateToAsync($"PayslipDetail?slipId={slip.Id}");
}
