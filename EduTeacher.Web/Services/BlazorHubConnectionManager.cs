using Rootfly.Mobile.Core.Networking.SignalR;
using Volo.Abp.DependencyInjection;

namespace EduTeacher.Web.Services;

#pragma warning disable CS0067
public class BlazorHubConnectionManager : IHubConnectionManager, ISingletonDependency
{
    public bool IsConnected => false;

    public event Action<Exception?>? ConnectionClosed;
    public event Action<string?>? Reconnecting;
    public event Action<string?>? Reconnected;

    public Task ConnectAsync(string hubUrl, CancellationToken ct = default)
    {
        return Task.CompletedTask;
    }

    public Task DisconnectAsync(CancellationToken ct = default)
    {
        return Task.CompletedTask;
    }

    public IDisposable On<T>(string method, Action<T> handler)
    {
        return new NoOpDisposable();
    }

    public IDisposable On(string method, Action handler)
    {
        return new NoOpDisposable();
    }

    public Task SendAsync(string method, object? arg = null, CancellationToken ct = default)
    {
        return Task.CompletedTask;
    }

    public Task<T> InvokeAsync<T>(string method, object? arg = null, CancellationToken ct = default)
    {
        return Task.FromResult(default(T)!);
    }

    private sealed class NoOpDisposable : IDisposable
    {
        public void Dispose() { }
    }
}
#pragma warning restore CS0067
