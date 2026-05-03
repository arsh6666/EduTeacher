using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Rootfly.Mobile.Core.Common.ViewModels;
using Rootfly.Mobile.Core.Security.Interfaces;
using EduTeacher.Shared.Payroll.Dtos;
using EduTeacher.Shared.Payroll.Services;

namespace EduTeacher.Shared.ViewModels;

public partial class TaxDeclarationViewModel : BaseViewModel
{
    private readonly IPayrollApiService _payrollApi;
    private readonly IAuthService _authService;

    [ObservableProperty] private ObservableCollection<EmployeeTaxDeclarationDto> _declarations = new();
    [ObservableProperty] private EmployeeTaxDeclarationDto? _currentDeclaration;
    [ObservableProperty] private ObservableCollection<TaxExemptionCategoryDto> _categories = new();

    public TaxDeclarationViewModel(IPayrollApiService payrollApi, IAuthService authService)
    {
        _payrollApi = payrollApi;
        _authService = authService;
        Title = "Tax Declaration";
    }

    public override async Task OnAppearingAsync()
    {
        await ExecuteBusyAsync(async () =>
        {
            var user = await _authService.GetCurrentUserAsync();
            if (user is null) return;

            var result = await _payrollApi.GetTaxDeclarationsAsync(user.Id);
            if (result.IsSuccess && result.Data is not null)
            {
                Declarations = new ObservableCollection<EmployeeTaxDeclarationDto>(result.Data.Items);
                CurrentDeclaration = result.Data.Items.FirstOrDefault(d => d.Status != TaxDeclarationStatus.Locked);
            }

            var cats = await _payrollApi.GetTaxExemptionCategoriesAsync();
            if (cats.IsSuccess && cats.Data is not null)
                Categories = new ObservableCollection<TaxExemptionCategoryDto>(cats.Data.Items);
        });
    }

    [RelayCommand]
    private async Task SubmitDeclaration()
    {
        if (CurrentDeclaration is null) return;
        await ExecuteBusyAsync(async () =>
        {
            var result = await _payrollApi.SubmitTaxDeclarationAsync(CurrentDeclaration.Id);
            if (result.IsSuccess)
            {
                await Dialog.ShowToastAsync("Tax declaration submitted");
                await OnAppearingAsync();
            }
            else ErrorMessage = result.Error ?? "Failed to submit";
        });
    }
}
