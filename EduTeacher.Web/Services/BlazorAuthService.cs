using Rootfly.Mobile.Core.Security.Interfaces;
using Rootfly.Mobile.Core.Security.Models;

namespace EduTeacher.Web.Services;

public class BlazorAuthService : IAuthService
{
    private bool _isAuthenticated;

    public bool IsAuthenticated => _isAuthenticated;

    public Task<AuthResult> LoginAsync(string email, string password)
    {
        _isAuthenticated = true;
        var token = new AuthToken
        {
            AccessToken = "mock-access-token",
            RefreshToken = "mock-refresh-token",
            ExpiresAt = DateTime.UtcNow.AddHours(1)
        };
        return Task.FromResult(AuthResult.Success(token));
    }

    public Task<AuthResult> RefreshTokenAsync()
    {
        var token = new AuthToken
        {
            AccessToken = "mock-refreshed-token",
            RefreshToken = "mock-refresh-token",
            ExpiresAt = DateTime.UtcNow.AddHours(1)
        };
        return Task.FromResult(AuthResult.Success(token));
    }

    public Task LogoutAsync()
    {
        _isAuthenticated = false;
        return Task.CompletedTask;
    }

    public Task<UserDto?> GetCurrentUserAsync()
    {
        if (!_isAuthenticated)
            return Task.FromResult<UserDto?>(null);

        return Task.FromResult<UserDto?>(new UserDto
        {
            Id = Guid.NewGuid(),
            Name = "Teacher User",
            Email = "teacher@test.com",
            AvatarUrl = null,
            Roles = ["Teacher"],
            Permissions = []
        });
    }
}
