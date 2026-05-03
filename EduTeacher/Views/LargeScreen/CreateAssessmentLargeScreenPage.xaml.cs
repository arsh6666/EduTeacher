using Rootfly.Mobile.Core.UI.Pages;
using EduTeacher.Shared.ViewModels;

namespace EduTeacher.Views.LargeScreen;

public partial class CreateAssessmentLargeScreenPage : BaseContentPage<CreateAssessmentViewModel>
{
    public CreateAssessmentLargeScreenPage(CreateAssessmentViewModel viewModel) : base(viewModel)
    {
        InitializeComponent();
    }
}
