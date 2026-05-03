using Rootfly.Mobile.Core.UI.Pages;
using EduTeacher.Shared.ViewModels;

namespace EduTeacher.Views.LargeScreen;

public partial class AssessmentListLargeScreenPage : BaseContentPage<AssessmentListViewModel>
{
    public AssessmentListLargeScreenPage(AssessmentListViewModel viewModel) : base(viewModel)
    {
        InitializeComponent();
    }
}
