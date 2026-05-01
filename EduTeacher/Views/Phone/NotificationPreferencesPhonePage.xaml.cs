using Rootfly.Mobile.Core.UI.Pages;
using EduTeacher.Shared.ViewModels;

namespace EduTeacher.Views.Phone;

public partial class NotificationPreferencesPhonePage : BaseContentPage<NotificationPreferencesViewModel>
{
    public NotificationPreferencesPhonePage(NotificationPreferencesViewModel viewModel) : base(viewModel)
    {
        InitializeComponent();
    }
}
