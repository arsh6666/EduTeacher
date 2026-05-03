using Rootfly.Mobile.Core.UI.Pages;
using EduTeacher.Shared.ViewModels;

namespace EduTeacher.Views.LargeScreen;

public partial class GradeSubmissionsLargeScreenPage : BaseContentPage<GradeSubmissionsViewModel>
{
    public GradeSubmissionsLargeScreenPage(GradeSubmissionsViewModel viewModel) : base(viewModel)
    {
        InitializeComponent();
    }
}
