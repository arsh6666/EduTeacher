using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Rootfly.Mobile.Core.Common.Abstractions;
using Rootfly.Mobile.Core.Common.ViewModels;
using EduTeacher.Shared.Education.Dtos;
using EduTeacher.Shared.Education.Services;
using System.Collections.ObjectModel;

namespace EduTeacher.Shared.ViewModels;

public partial class GradeBookViewModel : BaseViewModel
{
    private readonly ITeacherEducationApiService _educationApi;
    private readonly ILocalizationService _l;

    [ObservableProperty] private SectionDto? _selectedSection;
    [ObservableProperty] private InstructorDto? _instructor;

    public ObservableCollection<SectionDto> Sections { get; } = [];
    public ObservableCollection<StudentCourseGradeDto> Grades { get; } = [];

    public GradeBookViewModel(
        ITeacherEducationApiService educationApi,
        ILocalizationService localizationService)
    {
        _educationApi = educationApi;
        _l = localizationService;
        Title = "Grade Book";
    }

    public override async Task OnAppearingAsync()
    {
        await ExecuteBusyAsync(async () =>
        {
            Title = await _l.GetStringAsync("GradeBook");
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

                    if (Sections.Count > 0)
                    {
                        SelectedSection = Sections[0];
                        await LoadGradesAsync();
                    }
                }
            }
        });
    }

    [RelayCommand]
    private async Task SelectSectionAsync(SectionDto section)
    {
        SelectedSection = section;
        await LoadGradesAsync();
    }

    private async Task LoadGradesAsync()
    {
        if (SelectedSection is null) return;
        var result = await _educationApi.GetCourseGradesAsync(SelectedSection.Id);
        if (result.IsSuccess && result.Data is not null)
        {
            Grades.Clear();
            foreach (var g in result.Data.Items.OrderBy(x => x.StudentName))
                Grades.Add(g);
        }
    }
}
