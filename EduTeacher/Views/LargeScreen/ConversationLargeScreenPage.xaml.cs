using Rootfly.Mobile.Core.UI.Pages;
using EduTeacher.Shared.ViewModels;

namespace EduTeacher.Views.LargeScreen;

public partial class ConversationLargeScreenPage : BaseContentPage<ConversationViewModel>
{
    public ConversationLargeScreenPage(ConversationViewModel viewModel) : base(viewModel)
    {
        InitializeComponent();
    }
}
