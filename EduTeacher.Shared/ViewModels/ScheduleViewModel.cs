using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Rootfly.Mobile.Core.Common.Abstractions;
using Rootfly.Mobile.Core.Common.ViewModels;
using EduTeacher.Shared.Education.Dtos;
using EduTeacher.Shared.Education.Services;
using System.Collections.ObjectModel;

namespace EduTeacher.Shared.ViewModels;

public partial class ScheduleViewModel : BaseViewModel
{
    private readonly ITeacherEducationApiService _educationApi;
    private readonly ILocalizationService _l;

    [ObservableProperty] private DayOfWeek _selectedDay = DateTime.Now.DayOfWeek;
    [ObservableProperty] private InstructorDto? _instructor;

    public ObservableCollection<TimetableEntryDto> ScheduleEntries { get; } = [];

    public ScheduleViewModel(
        ITeacherEducationApiService educationApi,
        ILocalizationService localizationService)
    {
        _educationApi = educationApi;
        _l = localizationService;
        Title = "Schedule";
    }

    public override async Task OnAppearingAsync()
    {
        await ExecuteBusyAsync(async () =>
        {
            Title = await _l.GetStringAsync("Schedule");
            var instructorResult = await _educationApi.GetCurrentInstructorAsync();
            if (instructorResult.IsSuccess && instructorResult.Data is not null)
            {
                Instructor = instructorResult.Data;
                await LoadScheduleAsync();
            }
        });
    }

    [RelayCommand]
    private async Task SelectDayAsync(DayOfWeek day)
    {
        SelectedDay = day;
        await LoadScheduleAsync();
    }

    private async Task LoadScheduleAsync()
    {
        if (Instructor is null) return;
        var result = await _educationApi.GetMyScheduleAsync(Instructor.Id);
        if (result.IsSuccess && result.Data is not null)
        {
            var entries = result.Data.Items
                .Where(e => e.DayOfWeek == SelectedDay)
                .OrderBy(e => e.StartTime)
                .ToList();

            ScheduleEntries.Clear();
            foreach (var entry in entries)
                ScheduleEntries.Add(entry);
        }
    }
}
