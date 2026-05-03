using Rootfly.Mobile.Core.UI.Pages;
using EduTeacher.Shared.ViewModels;

namespace EduTeacher.Views.LargeScreen;

public partial class SalaryAdvanceLargeScreenPage : BaseContentPage<SalaryAdvanceViewModel>
{
    public SalaryAdvanceLargeScreenPage(SalaryAdvanceViewModel viewModel) : base(viewModel)
    {
        InitializeComponent();
    }
}
