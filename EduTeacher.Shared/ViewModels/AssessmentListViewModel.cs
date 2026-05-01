using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Rootfly.Mobile.Core.Common.Abstractions;
using Rootfly.Mobile.Core.Common.ViewModels;
using EduTeacher.Shared.Education.Dtos;
using EduTeacher.Shared.Education.Services;
using System.Collections.ObjectModel;

namespace EduTeacher.Shared.ViewModels;

public partial class AssessmentListViewModel : BaseViewModel
{
    private readonly ITeacherEducationApiService _educationApi;
    private readonly ILocalizationService _l;

    public ObservableCollection<AssessmentDto> Assessments { get; } = [];

    [ObservableProperty] private bool _isEmpty;

    public AssessmentListViewModel(
        ITeacherEducationApiService educationApi,
        ILocalizationService localizationService)
    {
        _educationApi = educationApi;
        _l = localizationService;
        Title = "Assessments";
    }

    public override async Task OnAppearingAsync()
    {
        await ExecuteBusyAsync(async () =>
        {
            Title = await _l.GetStringAsync("Assessments");
            await LoadAssessmentsAsync();
        });
    }

    private async Task LoadAssessmentsAsync()
    {
        var result = await _educationApi.GetMyAssessmentsAsync();
        if (result.IsSuccess && result.Data is not null)
        {
            Assessments.Clear();
            foreach (var a in result.Data.Items.OrderByDescending(x => x.CreationTime))
                Assessments.Add(a);
            IsEmpty = Assessments.Count == 0;
        }
    }

    [RelayCommand]
    private async Task NavigateToCreateAsync()
        => await Navigation.NavigateToAsync("CreateAssessment");

    [RelayCommand]
    private async Task NavigateToSubmissionsAsync(AssessmentDto assessment)
        => await Navigation.NavigateToAsync($"GradeSubmissions?assessmentId={assessment.Id}");

    [RelayCommand]
    private async Task RefreshAsync() => await LoadAssessmentsAsync();
}
