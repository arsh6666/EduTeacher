using Rootfly.Mobile.Core.Common.Abstractions;
using Volo.Abp.DependencyInjection;

namespace EduTeacher.Web.Services;

public class BlazorLocalizationService : ILocalizationService, ISingletonDependency
{
    private string _currentCulture = "en";
    private readonly IReadOnlyList<string> _supportedCultures = ["en", "es", "ar", "he"];

    public Task<string> GetStringAsync(string key)
    {
        return Task.FromResult(key);
    }

    public Task<string> GetStringAsync(string resourceName, string key)
    {
        return Task.FromResult(key);
    }

    public Task SetCultureAsync(string culture)
    {
        _currentCulture = culture;
        return Task.CompletedTask;
    }

    public string GetCurrentCulture() => _currentCulture;

    public IReadOnlyList<string> GetSupportedCultures() => _supportedCultures;
}
