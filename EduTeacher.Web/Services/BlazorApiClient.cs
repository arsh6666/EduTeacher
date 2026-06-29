using System.Net.Http.Json;
using Rootfly.Mobile.Core.Common.Results;
using Rootfly.Mobile.Core.Networking.REST;
using Volo.Abp.DependencyInjection;

namespace EduTeacher.Web.Services;

public class BlazorApiClient : IApiClient, ISingletonDependency
{
    private readonly HttpClient _httpClient;

    public BlazorApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ApiResult<T>> GetAsync<T>(string url, CancellationToken ct = default)
    {
        try
        {
            var response = await _httpClient.GetAsync(url, ct);
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<T>(ct);
                return ApiResult.Success(data!, (int)response.StatusCode);
            }
            return ApiResult.Failure<T>(response.ReasonPhrase ?? "Request failed", (int)response.StatusCode);
        }
        catch (Exception ex)
        {
            return ApiResult.Failure<T>(ex.Message);
        }
    }

    public async Task<ApiResult<T>> GetAsync<T>(string url, object queryParams, CancellationToken ct = default)
    {
        return await GetAsync<T>(url, ct);
    }

    public async Task<ApiResult<T>> PostAsync<T>(string url, object body, CancellationToken ct = default)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync(url, body, ct);
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<T>(ct);
                return ApiResult.Success(data!, (int)response.StatusCode);
            }
            return ApiResult.Failure<T>(response.ReasonPhrase ?? "Request failed", (int)response.StatusCode);
        }
        catch (Exception ex)
        {
            return ApiResult.Failure<T>(ex.Message);
        }
    }

    public async Task<ApiResult<T>> PutAsync<T>(string url, object body, CancellationToken ct = default)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync(url, body, ct);
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<T>(ct);
                return ApiResult.Success(data!, (int)response.StatusCode);
            }
            return ApiResult.Failure<T>(response.ReasonPhrase ?? "Request failed", (int)response.StatusCode);
        }
        catch (Exception ex)
        {
            return ApiResult.Failure<T>(ex.Message);
        }
    }

    public async Task<ApiResult<T>> PatchAsync<T>(string url, object body, CancellationToken ct = default)
    {
        try
        {
            var content = JsonContent.Create(body);
            var request = new HttpRequestMessage(HttpMethod.Patch, url) { Content = content };
            var response = await _httpClient.SendAsync(request, ct);
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<T>(ct);
                return ApiResult.Success(data!, (int)response.StatusCode);
            }
            return ApiResult.Failure<T>(response.ReasonPhrase ?? "Request failed", (int)response.StatusCode);
        }
        catch (Exception ex)
        {
            return ApiResult.Failure<T>(ex.Message);
        }
    }

    public async Task<ApiResult> DeleteAsync(string url, CancellationToken ct = default)
    {
        try
        {
            var response = await _httpClient.DeleteAsync(url, ct);
            if (response.IsSuccessStatusCode)
                return ApiResult.Success((int)response.StatusCode);
            return ApiResult.Failure(response.ReasonPhrase ?? "Request failed", (int)response.StatusCode);
        }
        catch (Exception ex)
        {
            return ApiResult.Failure(ex.Message);
        }
    }

    public async Task<ApiResult<PagedResult<T>>> GetPagedAsync<T>(string url, object? queryParams = null, CancellationToken ct = default)
    {
        try
        {
            var response = await _httpClient.GetAsync(url, ct);
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<PagedResult<T>>(ct);
                return ApiResult.Success(data!, (int)response.StatusCode);
            }
            return ApiResult.Failure<PagedResult<T>>(response.ReasonPhrase ?? "Request failed", (int)response.StatusCode);
        }
        catch (Exception ex)
        {
            return ApiResult.Failure<PagedResult<T>>(ex.Message);
        }
    }
}
