using Rootfly.Mobile.Core.UI.Pages;
using EduTeacher.Shared.ViewModels;

namespace EduTeacher.Views.LargeScreen;

public partial class ChatListLargeScreenPage : BaseContentPage<ChatListViewModel>
{
    public ChatListLargeScreenPage(ChatListViewModel viewModel) : base(viewModel)
    {
        InitializeComponent();
    }
}
