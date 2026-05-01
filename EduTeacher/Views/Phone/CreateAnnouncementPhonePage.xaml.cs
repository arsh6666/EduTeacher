using Rootfly.Mobile.Core.UI.Pages;
using EduTeacher.Shared.ViewModels;

namespace EduTeacher.Views.Phone;

public partial class CreateAnnouncementPhonePage : BaseContentPage<CreateAnnouncementViewModel>
{
    public CreateAnnouncementPhonePage(CreateAnnouncementViewModel viewModel) : base(viewModel)
    {
        InitializeComponent();
    }
}
