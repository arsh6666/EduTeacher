using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Rootfly.Mobile.Core.Common.Abstractions;
using Rootfly.Mobile.Core.Common.ViewModels;
using Rootfly.Mobile.Core.Networking.REST.ApplicationConfiguration;
using Rootfly.Mobile.Core.Security.Interfaces;
using Rootfly.Mobile.Core.Security.Models;
using EduTeacher.Shared.Education.Dtos;
using EduTeacher.Shared.Education.Services;
using System.Collections.ObjectModel;

namespace EduTeacher.Shared.ViewModels;

public partial class DashboardViewModel : BaseViewModel
{
    private readonly IAuthService _authService;
    private readonly ILocalizationService _l;
    private readonly IAbpApplicationConfigurationService _appConfig;
    private readonly ITeacherEducationApiService _educationApi;

    [ObservableProperty] private UserDto? _currentUser;
    [ObservableProperty] private string _welcomeMessage = string.Empty;
    [ObservableProperty] private string _todayDate = string.Empty;
    [ObservableProperty] private int _todayClassesCount;
    [ObservableProperty] private int _pendingAttendanceCount;
    [ObservableProperty] private int _recentSubmissionsCount;

    public ObservableCollection<TimetableEntryDto> TodaySchedule { get; } = [];

    private InstructorDto? _instructor;

    public DashboardViewModel(
        IAuthService authService,
        ILocalizationService localizationService,
        IAbpApplicationConfigurationService appConfig,
        ITeacherEducationApiService educationApi)
    {
        _authService = authService;
        _l = localizationService;
        _appConfig = appConfig;
        _educationApi = educationApi;
        Title = "Dashboard";
    }

    public override async Task OnAppearingAsync()
    {
        await ExecuteBusyAsync(async () =>
        {
            Title = await _l.GetStringAsync("Dashboard");
            TodayDate = DateTime.Now.ToString("dddd, MMMM dd");

            var user = await _authService.GetCurrentUserAsync();
            if (user is not null)
            {
                CurrentUser = user;
                var greeting = await _l.GetStringAsync("Welcome");
                WelcomeMessage = $"{greeting}, {CurrentUser.Name}!";
            }

            // Load instructor and schedule
            var instructorResult = await _educationApi.GetCurrentInstructorAsync();
            if (instructorResult.IsSuccess && instructorResult.Data is not null)
            {
                _instructor = instructorResult.Data;
                await LoadTodayScheduleAsync();
                await LoadPendingCountsAsync();
            }
        });
    }

    private async Task LoadTodayScheduleAsync()
    {
        if (_instructor is null) return;
        var result = await _educationApi.GetMyScheduleAsync(_instructor.Id);
        if (result.IsSuccess && result.Data is not null)
        {
            var today = DateTime.Now.DayOfWeek;
            var todayEntries = result.Data.Items
                .Where(e => e.DayOfWeek == today)
                .OrderBy(e => e.StartTime)
                .ToList();

            TodaySchedule.Clear();
            foreach (var entry in todayEntries)
                TodaySchedule.Add(entry);

            TodayClassesCount = todayEntries.Count;
        }
    }

    private async Task LoadPendingCountsAsync()
    {
        if (_instructor is null) return;
        var sectionsResult = await _educationApi.GetMySectionsAsync(_instructor.Id);
        if (sectionsResult.IsSuccess && sectionsResult.Data is not null)
        {
            PendingAttendanceCount = sectionsResult.Data.Items.Count;
        }

        var assessmentsResult = await _educationApi.GetMyAssessmentsAsync();
        if (assessmentsResult.IsSuccess && assessmentsResult.Data is not null)
        {
            RecentSubmissionsCount = assessmentsResult.Data.Items.Sum(a => a.SubmissionCount - a.GradedCount);
        }
    }

    [RelayCommand]
    private async Task NavigateToAttendanceAsync()
        => await Navigation.NavigateToAsync("//Attendance");

    [RelayCommand]
    private async Task NavigateToAssessmentsAsync()
        => await Navigation.NavigateToAsync("//Assessments");

    [RelayCommand]
    private async Task LogoutAsync()
    {
        await _authService.LogoutAsync();
        await Navigation.NavigateToAsync("//Login");
    }
}
