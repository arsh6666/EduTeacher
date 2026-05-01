using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Rootfly.Mobile.Core.Common.Abstractions;
using Rootfly.Mobile.Core.Common.ViewModels;
using EduTeacher.Shared.Education.Dtos;
using EduTeacher.Shared.Education.Services;

namespace EduTeacher.Shared.ViewModels;

public partial class NotificationPreferencesViewModel : BaseViewModel
{
    private readonly ITeacherEducationApiService _educationApi;
    private readonly ILocalizationService _l;

    [ObservableProperty] private bool _attendanceAlerts = true;
    [ObservableProperty] private bool _gradeAlerts = true;
    [ObservableProperty] private bool _feeAlerts = true;
    [ObservableProperty] private bool _assignmentAlerts = true;
    [ObservableProperty] private bool _announcementAlerts = true;
    [ObservableProperty] private bool _chatAlerts = true;
    [ObservableProperty] private bool _scheduleAlerts = true;
    [ObservableProperty] private bool _isSaving;

    public NotificationPreferencesViewModel(
        ITeacherEducationApiService educationApi,
        ILocalizationService localizationService)
    {
        _educationApi = educationApi;
        _l = localizationService;
        Title = "Notification Settings";
    }

    public override async Task OnAppearingAsync()
    {
        await ExecuteBusyAsync(LoadAsync);
    }

    [RelayCommand]
    private async Task LoadAsync()
    {
        var result = await _educationApi.GetNotificationPreferencesAsync();
        if (result.IsSuccess && result.Data is not null)
        {
            AttendanceAlerts = result.Data.AttendanceAlerts;
            GradeAlerts = result.Data.GradeAlerts;
            FeeAlerts = result.Data.FeeAlerts;
            AssignmentAlerts = result.Data.AssignmentAlerts;
            AnnouncementAlerts = result.Data.AnnouncementAlerts;
            ChatAlerts = result.Data.ChatAlerts;
            ScheduleAlerts = result.Data.ScheduleAlerts;
        }
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        IsSaving = true;
        var dto = new NotificationPreferenceDto
        {
            AttendanceAlerts = AttendanceAlerts,
            GradeAlerts = GradeAlerts,
            FeeAlerts = FeeAlerts,
            AssignmentAlerts = AssignmentAlerts,
            AnnouncementAlerts = AnnouncementAlerts,
            ChatAlerts = ChatAlerts,
            ScheduleAlerts = ScheduleAlerts
        };

        var result = await _educationApi.UpdateNotificationPreferencesAsync(dto);
        IsSaving = false;

        if (!result.IsSuccess)
            ErrorMessage = result.Error ?? "Failed to save preferences";
    }
}
