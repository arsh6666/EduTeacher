using Rootfly.Mobile.Core.UI.Pages;
using EduTeacher.Shared.ViewModels;

namespace EduTeacher.Views.LargeScreen;

public partial class TaxDeclarationLargeScreenPage : BaseContentPage<TaxDeclarationViewModel>
{
    public TaxDeclarationLargeScreenPage(TaxDeclarationViewModel viewModel) : base(viewModel)
    {
        InitializeComponent();
    }
}
