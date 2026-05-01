using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Rootfly.Mobile.Core.Common.Abstractions;
using Rootfly.Mobile.Core.Common.ViewModels;
using EduTeacher.Shared.Education.Dtos;
using EduTeacher.Shared.Education.Services;
using System.Collections.ObjectModel;

namespace EduTeacher.Shared.ViewModels;

public partial class MarkAttendanceViewModel : BaseViewModel
{
    private readonly ITeacherEducationApiService _educationApi;
    private readonly ILocalizationService _l;

    [ObservableProperty] private SectionDto? _selectedSection;
    [ObservableProperty] private DateTime _selectedDate = DateTime.Today;
    [ObservableProperty] private bool _isSubmitting;
    [ObservableProperty] private InstructorDto? _instructor;

    public ObservableCollection<SectionDto> Sections { get; } = [];
    public ObservableCollection<StudentAttendanceItem> Students { get; } = [];

    public MarkAttendanceViewModel(
        ITeacherEducationApiService educationApi,
        ILocalizationService localizationService)
    {
        _educationApi = educationApi;
        _l = localizationService;
        Title = "Mark Attendance";
    }

    public override async Task OnAppearingAsync()
    {
        await ExecuteBusyAsync(async () =>
        {
            Title = await _l.GetStringAsync("MarkAttendance");
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
        }
    }

    [RelayCommand]
    private async Task SelectSectionAsync(SectionDto section)
    {
        SelectedSection = section;
        await LoadStudentsAsync();
    }

    private async Task LoadStudentsAsync()
    {
        if (SelectedSection is null) return;
        var result = await _educationApi.GetSectionStudentsAsync(SelectedSection.Id);
        if (result.IsSuccess && result.Data is not null)
        {
            Students.Clear();
            foreach (var student in result.Data.Items)
            {
                Students.Add(new StudentAttendanceItem
                {
                    StudentId = student.Id,
                    StudentName = student.FullName,
                    RollNumber = student.RollNumber,
                    Status = AttendanceStatus.Present
                });
            }
        }
    }

    [RelayCommand]
    private void MarkAllPresent()
    {
        foreach (var student in Students)
            student.Status = AttendanceStatus.Present;
    }

    [RelayCommand]
    private async Task SubmitAttendanceAsync()
    {
        if (SelectedSection is null || Students.Count == 0) return;
        IsSubmitting = true;

        var input = new BulkAttendanceInput
        {
            Date = SelectedDate,
            SectionId = SelectedSection.Id,
            Records = Students.Select(s => new AttendanceRecordInput
            {
                StudentId = s.StudentId,
                Status = s.Status,
                Remarks = s.Remarks
            }).ToList()
        };

        var result = await _educationApi.BulkMarkAttendanceAsync(input);
        IsSubmitting = false;

        if (result.IsSuccess)
        {
            await Dialog.ShowAlertAsync("Success", "Attendance submitted successfully.", "OK");
            await Navigation.NavigateToAsync("//Dashboard");
        }
        else
        {
            ErrorMessage = result.Error ?? "Failed to submit attendance.";
        }
    }
}

public partial class StudentAttendanceItem : ObservableObject
{
    public Guid StudentId { get; set; }
    public string StudentName { get; set; } = string.Empty;
    public string? RollNumber { get; set; }
    [ObservableProperty] private AttendanceStatus _status = AttendanceStatus.Present;
    [ObservableProperty] private string? _remarks;
}
