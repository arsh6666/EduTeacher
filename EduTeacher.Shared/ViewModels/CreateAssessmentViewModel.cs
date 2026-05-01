using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Rootfly.Mobile.Core.Common.Abstractions;
using Rootfly.Mobile.Core.Common.ViewModels;
using EduTeacher.Shared.Education.Dtos;
using EduTeacher.Shared.Education.Services;
using System.Collections.ObjectModel;

namespace EduTeacher.Shared.ViewModels;

public partial class CreateAssessmentViewModel : BaseViewModel
{
    private readonly ITeacherEducationApiService _educationApi;
    private readonly ILocalizationService _l;

    [ObservableProperty] private string _assessmentTitle = string.Empty;
    [ObservableProperty] private string _description = string.Empty;
    [ObservableProperty] private string _assessmentType = "Assignment";
    [ObservableProperty] private DateTime _dueDate = DateTime.Today.AddDays(7);
    [ObservableProperty] private double _totalMarks = 100;
    [ObservableProperty] private double _weightPercentage;
    [ObservableProperty] private SectionDto? _selectedSection;
    [ObservableProperty] private InstructorDto? _instructor;
    [ObservableProperty] private bool _isSubmitting;

    public ObservableCollection<SectionDto> Sections { get; } = [];
    public List<string> AssessmentTypes { get; } = ["Assignment", "Quiz", "Project", "Presentation", "Lab"];

    public CreateAssessmentViewModel(
        ITeacherEducationApiService educationApi,
        ILocalizationService localizationService)
    {
        _educationApi = educationApi;
        _l = localizationService;
        Title = "Create Assessment";
    }

    public override async Task OnAppearingAsync()
    {
        await ExecuteBusyAsync(async () =>
        {
            Title = await _l.GetStringAsync("CreateAssessment");
            var instructorResult = await _educationApi.GetCurrentInstructorAsync();
            if (instructorResult.IsSuccess && instructorResult.Data is not null)
            {
                Instructor = instructorResult.Data;
                var sectionsResult = await _educationApi.GetMySectionsAsync(Instructor.Id);
                if (sectionsResult.IsSuccess && sectionsResult.Data is not null)
                {
                    Sections.Clear();
                    foreach (var s in sectionsResult.Data.Items)
                        Sections.Add(s);
                }
            }
        });
    }

    [RelayCommand]
    private async Task CreateAsync()
    {
        if (string.IsNullOrWhiteSpace(AssessmentTitle) || SelectedSection is null)
        {
            ErrorMessage = "Title and section are required.";
            return;
        }

        IsSubmitting = true;
        var input = new CreateAssessmentInput
        {
            Title = AssessmentTitle,
            Description = Description,
            AssessmentType = AssessmentType,
            DueDate = DueDate,
            TotalMarks = TotalMarks,
            WeightPercentage = WeightPercentage,
            CourseOfferingId = SelectedSection.Id // Section maps to course offering
        };

        var result = await _educationApi.CreateAssessmentAsync(input);
        IsSubmitting = false;

        if (result.IsSuccess)
        {
            await Dialog.ShowAlertAsync("Success", "Assessment created.", "OK");
            await Navigation.GoBackAsync();
        }
        else
        {
            ErrorMessage = result.Error ?? "Failed to create assessment.";
        }
    }
}
