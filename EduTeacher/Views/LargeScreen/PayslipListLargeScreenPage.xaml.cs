using Rootfly.Mobile.Core.UI.Pages;
using EduTeacher.Shared.ViewModels;

namespace EduTeacher.Views.LargeScreen;

public partial class PayslipListLargeScreenPage : BaseContentPage<PayslipListViewModel>
{
    public PayslipListLargeScreenPage(PayslipListViewModel viewModel) : base(viewModel)
    {
        InitializeComponent();
    }
}
