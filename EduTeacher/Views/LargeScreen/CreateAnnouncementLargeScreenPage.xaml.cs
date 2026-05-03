using Rootfly.Mobile.Core.UI.Pages;
using EduTeacher.Shared.ViewModels;

namespace EduTeacher.Views.LargeScreen;

public partial class CreateAnnouncementLargeScreenPage : BaseContentPage<CreateAnnouncementViewModel>
{
    public CreateAnnouncementLargeScreenPage(CreateAnnouncementViewModel viewModel) : base(viewModel)
    {
        InitializeComponent();
    }
}
