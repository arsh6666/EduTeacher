using Rootfly.Mobile.Core.Common.Abstractions;
using Rootfly.Mobile.Core.UI.Pages;
using EduTeacher.Shared.ViewModels;

namespace EduTeacher.Views.Phone;

public partial class TaxDeclarationPhonePage : BaseContentPage<TaxDeclarationViewModel>
{
    public TaxDeclarationPhonePage(TaxDeclarationViewModel viewModel, INavigationService navigation, IDialogService dialog, IDeviceInfoService deviceInfo)
        : base(viewModel, navigation, dialog, deviceInfo)
    {
        InitializeComponent();
    }
}
