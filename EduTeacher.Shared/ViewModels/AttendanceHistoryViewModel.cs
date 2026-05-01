using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Rootfly.Mobile.Core.Common.Abstractions;
using Rootfly.Mobile.Core.Common.ViewModels;
using EduTeacher.Shared.Education.Dtos;
using EduTeacher.Shared.Education.Services;
using System.Collections.ObjectModel;

namespace EduTeacher.Shared.ViewModels;

public partial class AttendanceHistoryViewModel : BaseViewModel
{
    private readonly ITeacherEducationApiService _educationApi;
    private readonly ILocalizationService _l;

    [ObservableProperty] private SectionDto? _selectedSection;
    [ObservableProperty] private SectionAttendanceSummaryDto? _summary;
    [ObservableProperty] private InstructorDto? _instructor;

    public ObservableCollection<SectionDto> Sections { get; } = [];

    public AttendanceHistoryViewModel(
        ITeacherEducationApiService educationApi,
        ILocalizationService localizationService)
    {
        _educationApi = educationApi;
        _l = localizationService;
        Title = "Attendance History";
    }

    public override async Task OnAppearingAsync()
    {
        await ExecuteBusyAsync(async () =>
        {
            Title = await _l.GetStringAsync("AttendanceHistory");
            var instructorResult = await _educationApi.GetCurrentInstructorAsync();
            if (instructorResult.IsSuccess && instructorResult.Data is not null)
            {
                Instructor = instructorResult.Data;
                await LoadSectionsAsync();
            }
        });
    }

    private async Task LoadSectionsAsync()
    {
        if (Instructor is null) return;
        var result = await _educationApi.GetMySectionsAsync(Instructor.Id);
        if (result.IsSuccess && result.Data is not null)
        {
            Sections.Clear();
            foreach (var section in result.Data.Items)
                Sections.Add(section);

            if (Sections.Count > 0)
            {
                SelectedSection = Sections[0];
                await LoadSummaryAsync();
            }
        }
    }

    [RelayCommand]
    private async Task SelectSectionAsync(SectionDto section)
    {
        SelectedSection = section;
        await LoadSummaryAsync();
    }

    private async Task LoadSummaryAsync()
    {
        if (SelectedSection is null) return;
        var result = await _educationApi.GetSectionAttendanceSummaryAsync(SelectedSection.Id);
        if (result.IsSuccess)
            Summary = result.Data;
    }
}
