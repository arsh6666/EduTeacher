using Rootfly.Mobile.Core.UI.Pages;
using EduTeacher.Shared.ViewModels;

namespace EduTeacher.Views.Phone;

public partial class AnnouncementsManagePhonePage : BaseContentPage<AnnouncementManageViewModel>
{
    public AnnouncementsManagePhonePage(AnnouncementManageViewModel viewModel) : base(viewModel)
    {
        InitializeComponent();
    }
}
