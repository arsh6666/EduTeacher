using Rootfly.Mobile.Core.UI.Pages;
using EduTeacher.Shared.ViewModels;

namespace EduTeacher.Views.LargeScreen;

public partial class OvertimeLargeScreenPage : BaseContentPage<OvertimeViewModel>
{
    public OvertimeLargeScreenPage(OvertimeViewModel viewModel) : base(viewModel)
    {
        InitializeComponent();
    }
}
