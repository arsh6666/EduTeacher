using Rootfly.Mobile.Core.UI.Pages;
using EduTeacher.Shared.ViewModels;

namespace EduTeacher.Views.LargeScreen;

public partial class AttendanceLargeScreenPage : BaseContentPage<MarkAttendanceViewModel>
{
    public AttendanceLargeScreenPage(MarkAttendanceViewModel viewModel) : base(viewModel)
    {
        InitializeComponent();
    }
}
