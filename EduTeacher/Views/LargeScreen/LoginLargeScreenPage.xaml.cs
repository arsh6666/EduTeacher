using Rootfly.Mobile.Core.UI.Pages;
using EduTeacher.Shared.ViewModels;

namespace EduTeacher.Views.LargeScreen;

public partial class LoginLargeScreenPage : BaseContentPage<LoginViewModel>
{
    public LoginLargeScreenPage(LoginViewModel viewModel) : base(viewModel)
    {
        InitializeComponent();
    }
}
