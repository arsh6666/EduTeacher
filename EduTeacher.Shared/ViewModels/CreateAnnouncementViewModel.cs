using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Rootfly.Mobile.Core.Common.Abstractions;
using Rootfly.Mobile.Core.Common.ViewModels;
using EduTeacher.Shared.Education.Dtos;
using EduTeacher.Shared.Education.Services;

namespace EduTeacher.Shared.ViewModels;

public partial class CreateAnnouncementViewModel : BaseViewModel
{
    private readonly ITeacherEducationApiService _educationApi;
    private readonly ILocalizationService _l;

    [ObservableProperty] private string _announcementTitle = string.Empty;
    [ObservableProperty] private string _content = string.Empty;
    [ObservableProperty] private string _audience = "All";
    [ObservableProperty] private bool _isSubmitting;

    public List<string> AudienceOptions { get; } = ["All", "Student", "Parent", "Teacher"];

    public CreateAnnouncementViewModel(
        ITeacherEducationApiService educationApi,
        ILocalizationService localizationService)
    {
        _educationApi = educationApi;
        _l = localizationService;
        Title = "Create Announcement";
    }

    [RelayCommand]
    private async Task CreateAndPublishAsync()
    {
        if (string.IsNullOrWhiteSpace(AnnouncementTitle) || string.IsNullOrWhiteSpace(Content))
        {
            ErrorMessage = "Title and content are required";
            return;
        }

        IsSubmitting = true;
        var input = new CreateAnnouncementInput
        {
            Title = AnnouncementTitle,
            Content = Content,
            Audience = Audience
        };

        var result = await _educationApi.CreateAnnouncementAsync(input);
        if (result.IsSuccess && result.Data is not null)
        {
            await _educationApi.PublishAnnouncementAsync(result.Data.Id);
            await Navigation.GoBackAsync();
        }
        else
        {
            ErrorMessage = result.Error ?? "Failed to create announcement";
        }
        IsSubmitting = false;
    }

    [RelayCommand]
    private async Task SaveDraftAsync()
    {
        if (string.IsNullOrWhiteSpace(AnnouncementTitle))
        {
            ErrorMessage = "Title is required";
            return;
        }

        IsSubmitting = true;
        var input = new CreateAnnouncementInput
        {
            Title = AnnouncementTitle,
            Content = Content,
            Audience = Audience
        };

        var result = await _educationApi.CreateAnnouncementAsync(input);
        IsSubmitting = false;

        if (result.IsSuccess)
            await Navigation.GoBackAsync();
        else
            ErrorMessage = result.Error ?? "Failed to save draft";
    }
}
