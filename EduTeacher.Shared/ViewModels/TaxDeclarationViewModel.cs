using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Rootfly.Mobile.Core.Common.Abstractions;
using Rootfly.Mobile.Core.Common.ViewModels;
using EduTeacher.Shared.Education.Dtos;
using EduTeacher.Shared.Education.Services;
using System.Collections.ObjectModel;

namespace EduTeacher.Shared.ViewModels;

public partial class TaxDeclarationViewModel : BaseViewModel
{
    private readonly IPayrollApiService _payrollApi;
    private readonly ITeacherEducationApiService _educationApi;
    private readonly ILocalizationService _l;

    [ObservableProperty] private TaxDeclarationDto? _declaration;
    [ObservableProperty] private string _fiscalYear = string.Empty;
    [ObservableProperty] private bool _canSubmit;

    public ObservableCollection<TaxDeclarationLineDto> Lines { get; } = [];

    public TaxDeclarationViewModel(
        IPayrollApiService payrollApi,
        ITeacherEducationApiService educationApi,
        ILocalizationService localizationService)
    {
        _payrollApi = payrollApi;
        _educationApi = educationApi;
        _l = localizationService;
        Title = "Tax Declaration";
    }

    public override async Task OnAppearingAsync()
    {
        await ExecuteBusyAsync(async () =>
        {
            Title = await _l.GetStringAsync("TaxDeclaration");
            FiscalYear = GetCurrentFiscalYear();

            var instructorResult = await _educationApi.GetCurrentInstructorAsync();
            if (!instructorResult.IsSuccess || instructorResult.Data?.EmployeeId is null) return;

            var employeeId = instructorResult.Data.EmployeeId.Value;
            var result = await _payrollApi.GetTaxDeclarationAsync(employeeId, FiscalYear);
            if (result.IsSuccess && result.Data is not null)
            {
                Declaration = result.Data;
                CanSubmit = Declaration.Status == "Draft";
                Lines.Clear();
                foreach (var line in Declaration.Declarations)
                    Lines.Add(line);
            }
        });
    }

    [RelayCommand]
    private async Task SubmitAsync()
    {
        if (Declaration is null) return;
        var result = await _payrollApi.SubmitTaxDeclarationAsync(Declaration.Id);
        if (result.IsSuccess)
        {
            CanSubmit = false;
            await Dialog.ShowAlertAsync("Success", "Tax declaration submitted.", "OK");
        }
        else
        {
            ErrorMessage = result.Error ?? "Failed to submit.";
        }
    }

    private static string GetCurrentFiscalYear()
    {
        var now = DateTime.Now;
        var startYear = now.Month >= 4 ? now.Year : now.Year - 1;
        return $"{startYear}-{startYear + 1}";
    }
}
