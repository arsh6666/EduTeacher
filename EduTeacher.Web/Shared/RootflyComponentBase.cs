using Microsoft.AspNetCore.Components;
using Rootfly.Mobile.Core.Common.Abstractions;
using Rootfly.Mobile.Core.Common.ViewModels;

namespace EduTeacher.Web.Shared;

public abstract class RootflyComponentBase<TViewModel> : ComponentBase, IDisposable
    where TViewModel : BaseViewModel
{
    [Inject] public TViewModel ViewModel { get; set; } = default!;
    [Inject] public INavigationService Navigation { get; set; } = default!;
    [Inject] public IDialogService Dialog { get; set; } = default!;
    [Inject] public IDeviceInfoService DeviceInfo { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        ViewModel.PropertyChanged += OnViewModelPropertyChanged;
        await ViewModel.OnAppearingAsync();
    }

    private void OnViewModelPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        InvokeAsync(StateHasChanged);
    }

    public void Dispose()
    {
        ViewModel.PropertyChanged -= OnViewModelPropertyChanged;
        _ = ViewModel.OnDisappearingAsync();
    }
}
