using MudBlazor;
using IDialogService = Rootfly.Mobile.Core.Common.Abstractions.IDialogService;

namespace EduTeacher.Web.Services;

public class BlazorDialogService : IDialogService
{
    private readonly MudBlazor.IDialogService _mudDialogService;
    private readonly ISnackbar _snackbar;

    public BlazorDialogService(MudBlazor.IDialogService mudDialogService, ISnackbar snackbar)
    {
        _mudDialogService = mudDialogService;
        _snackbar = snackbar;
    }

    public async Task ShowAlertAsync(string title, string message, string cancel = "OK")
    {
        await _mudDialogService.ShowMessageBox(title, message, yesText: cancel);
    }

    public async Task<bool> ShowConfirmAsync(string title, string message, string accept = "Yes", string cancel = "No")
    {
        var result = await _mudDialogService.ShowMessageBox(title, message, yesText: accept, cancelText: cancel);
        return result == true;
    }

    public Task<string?> ShowActionSheetAsync(string title, string cancel, string? destruction, params string[] buttons)
    {
        // Simplified for Blazor - return null
        return Task.FromResult<string?>(null);
    }

    public Task ShowToastAsync(string message)
    {
        _snackbar.Add(message, Severity.Info);
        return Task.CompletedTask;
    }

    public Task ShowLoadingAsync(string? message = null)
    {
        return Task.CompletedTask;
    }

    public void HideLoading()
    {
    }
}
