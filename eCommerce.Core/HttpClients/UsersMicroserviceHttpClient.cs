using eCommerce.Core.DTO;
using System.Net.Http.Json;

namespace eCommerce.Core.HttpClients;

public class UsersMicroserviceHttpClient
{
    private readonly HttpClient _httpClient;

    public UsersMicroserviceHttpClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<UserDTO?> GetUserByIdAsync(Guid userId)
    {
        var response = await _httpClient.GetAsync($"{userId}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<UserDTO>();
    }

    public async Task<bool> IsUserExistsAsync(Guid userId)
    {
        var user = await GetUserByIdAsync(userId);
        return user != null;
    }
} 