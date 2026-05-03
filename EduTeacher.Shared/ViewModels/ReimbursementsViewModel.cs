using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Rootfly.Mobile.Core.Common.ViewModels;
using Rootfly.Mobile.Core.Security.Interfaces;
using EduTeacher.Shared.Payroll.Dtos;
using EduTeacher.Shared.Payroll.Services;

namespace EduTeacher.Shared.ViewModels;

public partial class ReimbursementsViewModel : BaseViewModel
{
    private readonly IPayrollApiService _payrollApi;
    private readonly IAuthService _authService;

    [ObservableProperty] private ObservableCollection<ReimbursementClaimDto> _claims = new();
    [ObservableProperty] private ObservableCollection<ReimbursementTypeDto> _types = new();
    [ObservableProperty] private int _pendingCount;
    [ObservableProperty] private int _approvedCount;
    [ObservableProperty] private decimal _totalApproved;

    public ReimbursementsViewModel(IPayrollApiService payrollApi, IAuthService authService)
    {
        _payrollApi = payrollApi;
        _authService = authService;
        Title = "Reimbursements";
    }

    public override async Task OnAppearingAsync()
    {
        await ExecuteBusyAsync(async () =>
        {
            var user = await _authService.GetCurrentUserAsync();
            if (user is null) return;

            var result = await _payrollApi.GetReimbursementsAsync(user.Id);
            if (result.IsSuccess && result.Data is not null)
            {
                Claims = new ObservableCollection<ReimbursementClaimDto>(result.Data.Items);
                PendingCount = result.Data.Items.Count(c => c.Status is ReimbursementStatus.Draft or ReimbursementStatus.Submitted);
                ApprovedCount = result.Data.Items.Count(c => c.Status == ReimbursementStatus.Approved);
                TotalApproved = result.Data.Items.Where(c => c.Status is ReimbursementStatus.Approved or ReimbursementStatus.Paid).Sum(c => c.TotalAmount);
            }

            var types = await _payrollApi.GetReimbursementTypesAsync();
            if (types.IsSuccess && types.Data is not null)
                Types = new ObservableCollection<ReimbursementTypeDto>(types.Data.Items);
        });
    }

    [RelayCommand]
    private Task NavigateToNewClaim() => Navigation.NavigateToAsync("NewReimbursement");
}
