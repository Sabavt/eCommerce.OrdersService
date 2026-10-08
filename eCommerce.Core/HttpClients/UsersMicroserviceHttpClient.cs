using eCommerce.Core.DTO;
using Microsoft.Extensions.Caching.Distributed;
using System.Net.Http.Json;
using System.Text.Json;

namespace eCommerce.Core.HttpClients;

public class UsersMicroserviceHttpClient
{
    private readonly HttpClient _httpClient;
    private readonly IDistributedCache _distributedCache;

    public UsersMicroserviceHttpClient(HttpClient httpClient, IDistributedCache distributedCache)
    {
        _httpClient = httpClient;
        _distributedCache = distributedCache;
    }

    public async Task<UserDTO?> GetUserByIdAsync(Guid userId)
    {
        string? user = await _distributedCache.GetStringAsync($"user:{userId}");
        if (user is not null)
        {
            var user_from_cache = JsonSerializer.Deserialize<UserDTO>(user);
            return user_from_cache;
        }

        var response = await _httpClient.GetAsync($"{userId}");
        response.EnsureSuccessStatusCode();
        var user_from_response = await response.Content.ReadFromJsonAsync<UserDTO>();

        if (response != null && response.StatusCode != System.Net.HttpStatusCode.ServiceUnavailable)
        {
            var user_json = JsonSerializer.Serialize(user_from_response);
            await _distributedCache.SetStringAsync($"user:{userId}", user_json, new DistributedCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(2)).SetSlidingExpiration(TimeSpan.FromMinutes(1)));
        }
        return user_from_response;
    }

    public async Task<bool> IsUserExistsAsync(Guid userId)
    {
        var user = await GetUserByIdAsync(userId);
        return user != null;
    }
}