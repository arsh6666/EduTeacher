using Rootfly.Mobile.Core.UI.Pages;
using EduTeacher.Shared.ViewModels;

namespace EduTeacher.Views.LargeScreen;

public partial class ScheduleLargeScreenPage : BaseContentPage<ScheduleViewModel>
{
    public ScheduleLargeScreenPage(ScheduleViewModel viewModel) : base(viewModel)
    {
        InitializeComponent();
    }
}
