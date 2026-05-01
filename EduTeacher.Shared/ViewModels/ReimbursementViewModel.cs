using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Rootfly.Mobile.Core.Common.Abstractions;
using Rootfly.Mobile.Core.Common.ViewModels;
using EduTeacher.Shared.Education.Dtos;
using EduTeacher.Shared.Education.Services;
using System.Collections.ObjectModel;

namespace EduTeacher.Shared.ViewModels;

public partial class ReimbursementViewModel : BaseViewModel
{
    private readonly IPayrollApiService _payrollApi;
    private readonly ITeacherEducationApiService _educationApi;
    private readonly ILocalizationService _l;

    public ObservableCollection<ReimbursementClaimDto> Claims { get; } = [];

    [ObservableProperty] private string _claimType = string.Empty;
    [ObservableProperty] private decimal _claimAmount;
    [ObservableProperty] private string _claimDescription = string.Empty;
    [ObservableProperty] private bool _isSubmitting;

    public ReimbursementViewModel(
        IPayrollApiService payrollApi,
        ITeacherEducationApiService educationApi,
        ILocalizationService localizationService)
    {
        _payrollApi = payrollApi;
        _educationApi = educationApi;
        _l = localizationService;
        Title = "Reimbursements";
    }

    public override async Task OnAppearingAsync()
    {
        await ExecuteBusyAsync(async () =>
        {
            Title = await _l.GetStringAsync("Reimbursements");

            var instructorResult = await _educationApi.GetCurrentInstructorAsync();
            if (!instructorResult.IsSuccess || instructorResult.Data?.EmployeeId is null) return;

            var employeeId = instructorResult.Data.EmployeeId.Value;
            var result = await _payrollApi.GetReimbursementClaimsAsync(employeeId);
            if (result.IsSuccess && result.Data is not null)
            {
                Claims.Clear();
                foreach (var claim in result.Data.Items)
                    Claims.Add(claim);
            }
        });
    }

    [RelayCommand]
    private async Task SubmitClaimAsync()
    {
        if (string.IsNullOrWhiteSpace(ClaimType) || ClaimAmount <= 0) return;

        IsSubmitting = true;
        var input = new ReimbursementClaimDto
        {
            Type = ClaimType,
            Amount = ClaimAmount,
            Description = ClaimDescription,
            RequestDate = DateTime.Now
        };

        var result = await _payrollApi.SubmitReimbursementClaimAsync(input);
        IsSubmitting = false;

        if (result.IsSuccess && result.Data is not null)
        {
            Claims.Insert(0, result.Data);
            ClaimType = string.Empty;
            ClaimAmount = 0;
            ClaimDescription = string.Empty;
            await Dialog.ShowAlertAsync("Success", "Claim submitted.", "OK");
        }
        else
        {
            ErrorMessage = result.Error ?? "Failed to submit claim.";
        }
    }
}
