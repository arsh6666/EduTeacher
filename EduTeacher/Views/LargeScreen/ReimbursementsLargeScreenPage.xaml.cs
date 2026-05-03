using Rootfly.Mobile.Core.UI.Pages;
using EduTeacher.Shared.ViewModels;

namespace EduTeacher.Views.LargeScreen;

public partial class ReimbursementsLargeScreenPage : BaseContentPage<ReimbursementsViewModel>
{
    public ReimbursementsLargeScreenPage(ReimbursementsViewModel viewModel) : base(viewModel)
    {
        InitializeComponent();
    }
}
