using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Rootfly.Mobile.Core.Common.Abstractions;
using Rootfly.Mobile.Core.Common.ViewModels;
using EduTeacher.Shared.Education.Dtos;
using EduTeacher.Shared.Education.Services;
using System.Collections.ObjectModel;

namespace EduTeacher.Shared.ViewModels;

public partial class GradeSubmissionsViewModel : BaseViewModel
{
    private readonly ITeacherEducationApiService _educationApi;
    private readonly ILocalizationService _l;

    [ObservableProperty] private string _assessmentId = string.Empty;
    [ObservableProperty] private int _totalSubmissions;
    [ObservableProperty] private int _gradedCount;

    public ObservableCollection<SubmissionGradeItem> Submissions { get; } = [];

    public GradeSubmissionsViewModel(
        ITeacherEducationApiService educationApi,
        ILocalizationService localizationService)
    {
        _educationApi = educationApi;
        _l = localizationService;
        Title = "Grade Submissions";
    }

    public override async Task OnAppearingAsync()
    {
        await ExecuteBusyAsync(async () =>
        {
            Title = await _l.GetStringAsync("GradeSubmissions");
            if (Guid.TryParse(AssessmentId, out var id))
                await LoadSubmissionsAsync(id);
        });
    }

    private async Task LoadSubmissionsAsync(Guid assessmentId)
    {
        var result = await _educationApi.GetSubmissionsAsync(assessmentId);
        if (result.IsSuccess && result.Data is not null)
        {
            Submissions.Clear();
            foreach (var sub in result.Data.Items.OrderBy(s => s.StudentName))
            {
                Submissions.Add(new SubmissionGradeItem
                {
                    SubmissionId = sub.Id,
                    StudentName = sub.StudentName ?? "Unknown",
                    SubmittedAt = sub.SubmittedAt,
                    Score = sub.Score,
                    Feedback = sub.Feedback,
                    IsGraded = sub.IsGraded
                });
            }
            TotalSubmissions = Submissions.Count;
            GradedCount = Submissions.Count(s => s.IsGraded);
        }
    }

    [RelayCommand]
    private async Task GradeAsync(SubmissionGradeItem item)
    {
        if (item.Score is null) return;

        var result = await _educationApi.GradeSubmissionAsync(item.SubmissionId, item.Score.Value, item.Feedback);
        if (result.IsSuccess)
        {
            item.IsGraded = true;
            GradedCount = Submissions.Count(s => s.IsGraded);
        }
        else
        {
            ErrorMessage = result.Error ?? "Failed to grade submission.";
        }
    }
}

public partial class SubmissionGradeItem : ObservableObject
{
    public Guid SubmissionId { get; set; }
    public string StudentName { get; set; } = string.Empty;
    public DateTime SubmittedAt { get; set; }
    [ObservableProperty] private double? _score;
    [ObservableProperty] private string? _feedback;
    [ObservableProperty] private bool _isGraded;
}
