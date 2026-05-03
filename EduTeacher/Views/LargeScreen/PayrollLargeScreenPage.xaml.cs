using Rootfly.Mobile.Core.UI.Pages;
using EduTeacher.Shared.ViewModels;

namespace EduTeacher.Views.LargeScreen;

public partial class PayrollLargeScreenPage : BaseContentPage<PayrollDashboardViewModel>
{
    public PayrollLargeScreenPage(PayrollDashboardViewModel viewModel) : base(viewModel)
    {
        InitializeComponent();
    }
}
