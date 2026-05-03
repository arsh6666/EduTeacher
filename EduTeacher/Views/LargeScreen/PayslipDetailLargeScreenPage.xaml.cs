using Rootfly.Mobile.Core.UI.Pages;
using EduTeacher.Shared.ViewModels;

namespace EduTeacher.Views.LargeScreen;

public partial class PayslipDetailLargeScreenPage : BaseContentPage<PayslipDetailViewModel>
{
    public PayslipDetailLargeScreenPage(PayslipDetailViewModel viewModel) : base(viewModel)
    {
        InitializeComponent();
    }
}
