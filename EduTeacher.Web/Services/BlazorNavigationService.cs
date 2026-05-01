using Microsoft.AspNetCore.Components;
using Rootfly.Mobile.Core.Common.Abstractions;

namespace EduTeacher.Web.Services;

public class BlazorNavigationService : INavigationService
{
    private readonly NavigationManager _navigationManager;

    public BlazorNavigationService(NavigationManager navigationManager)
    {
        _navigationManager = navigationManager;
    }

    public Task NavigateToAsync<TViewModel>(object? parameter = null) where TViewModel : class
    {
        var route = typeof(TViewModel).Name.Replace("ViewModel", "").ToLowerInvariant();
        _navigationManager.NavigateTo($"/{route}");
        return Task.CompletedTask;
    }

    public Task NavigateToAsync(string route, object? parameter = null)
    {
        _navigationManager.NavigateTo(route);
        return Task.CompletedTask;
    }

    public Task GoBackAsync()
    {
        _navigationManager.NavigateTo("javascript:history.back()");
        return Task.CompletedTask;
    }

    public Task NavigateToRootAsync()
    {
        _navigationManager.NavigateTo("/");
        return Task.CompletedTask;
    }
}
