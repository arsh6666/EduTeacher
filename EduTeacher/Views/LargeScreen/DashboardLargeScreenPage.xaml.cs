using Rootfly.Mobile.Core.UI.Pages;
using EduTeacher.Shared.ViewModels;

namespace EduTeacher.Views.LargeScreen;

public partial class DashboardLargeScreenPage : BaseContentPage<DashboardViewModel>
{
    public DashboardLargeScreenPage(DashboardViewModel viewModel) : base(viewModel)
    {
        InitializeComponent();
    }
}
