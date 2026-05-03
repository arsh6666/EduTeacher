using Rootfly.Mobile.Core.UI.Pages;
using EduTeacher.Shared.ViewModels;

namespace EduTeacher.Views.LargeScreen;

public partial class AnnouncementsManageLargeScreenPage : BaseContentPage<AnnouncementManageViewModel>
{
    public AnnouncementsManageLargeScreenPage(AnnouncementManageViewModel viewModel) : base(viewModel)
    {
        InitializeComponent();
    }
}
