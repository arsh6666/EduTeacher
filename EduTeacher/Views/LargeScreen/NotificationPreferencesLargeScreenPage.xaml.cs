using Rootfly.Mobile.Core.UI.Pages;
using EduTeacher.Shared.ViewModels;

namespace EduTeacher.Views.LargeScreen;

public partial class NotificationPreferencesLargeScreenPage : BaseContentPage<NotificationPreferencesViewModel>
{
    public NotificationPreferencesLargeScreenPage(NotificationPreferencesViewModel viewModel) : base(viewModel)
    {
        InitializeComponent();
    }
}
