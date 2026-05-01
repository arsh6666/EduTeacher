using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using Rootfly.Mobile.Core.Common.Abstractions;
using Rootfly.Mobile.Core.Common.ViewModels;
using EduTeacher.Shared.Education.Dtos;
using EduTeacher.Shared.Education.Services;

namespace EduTeacher.Shared.ViewModels;

public partial class AnnouncementManageViewModel : BaseViewModel
{
    private readonly ITeacherEducationApiService _educationApi;
    private readonly ILocalizationService _l;

    [ObservableProperty] private bool _isEmpty;

    public ObservableCollection<AnnouncementDto> Announcements { get; } = [];

    public AnnouncementManageViewModel(
        ITeacherEducationApiService educationApi,
        ILocalizationService localizationService)
    {
        _educationApi = educationApi;
        _l = localizationService;
        Title = "Announcements";
    }

    public override async Task OnAppearingAsync()
    {
        await ExecuteBusyAsync(LoadAsync);
    }

    [RelayCommand]
    private async Task LoadAsync()
    {
        var result = await _educationApi.GetAnnouncementsAsync();
        Announcements.Clear();
        if (result.IsSuccess && result.Data is not null)
        {
            foreach (var ann in result.Data.Items)
                Announcements.Add(ann);
        }
        IsEmpty = Announcements.Count == 0;
    }

    [RelayCommand]
    private async Task PublishAsync(AnnouncementDto announcement)
    {
        if (announcement.IsPublished) return;

        var result = await _educationApi.PublishAnnouncementAsync(announcement.Id);
        if (result.IsSuccess)
        {
            var index = Announcements.IndexOf(announcement);
            if (index >= 0)
            {
                announcement.IsPublished = true;
                Announcements.RemoveAt(index);
                Announcements.Insert(index, announcement);
            }
        }
        else
        {
            ErrorMessage = result.Error ?? "Failed to publish";
        }
    }

    [RelayCommand]
    private async Task CreateNewAsync()
    {
        await Navigation.NavigateToAsync("CreateAnnouncement");
    }

    [RelayCommand]
    private async Task RefreshAsync()
    {
        await ExecuteBusyAsync(LoadAsync);
    }
}
