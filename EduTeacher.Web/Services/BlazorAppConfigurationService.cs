using Rootfly.Mobile.Core.Networking.REST.ApplicationConfiguration;
using Rootfly.Mobile.Core.Networking.REST.ApplicationConfiguration.Dtos;
using Volo.Abp.DependencyInjection;

namespace EduTeacher.Web.Services;

[ExposeServices(typeof(IAbpApplicationConfigurationService))]
public class BlazorAppConfigurationService : IAbpApplicationConfigurationService, ISingletonDependency
{
    private AbpApplicationConfigurationDto? _config;

    public AbpCurrentUserDto? CurrentUser => _config?.CurrentUser;
    public AbpCurrentTenantDto? CurrentTenant => _config?.CurrentTenant;
    public bool IsLoaded => _config is not null;

    public Task<AbpApplicationConfigurationDto> GetAsync(CancellationToken ct = default)
    {
        _config ??= new AbpApplicationConfigurationDto();
        return Task.FromResult(_config);
    }

    public Task<AbpApplicationConfigurationDto> RefreshAsync(CancellationToken ct = default)
    {
        _config = new AbpApplicationConfigurationDto();
        return Task.FromResult(_config);
    }

    public string? GetSetting(string key)
    {
        if (_config?.Setting?.Values is null) return null;
        _config.Setting.Values.TryGetValue(key, out var val);
        return val;
    }

    public string? GetFeature(string key)
    {
        if (_config?.Features?.Values is null) return null;
        _config.Features.Values.TryGetValue(key, out var val);
        return val;
    }

    public bool IsGranted(string policyName)
    {
        return _config?.Auth?.GrantedPolicies?.ContainsKey(policyName) ?? false;
    }

    public void Clear()
    {
        _config = null;
    }
}
