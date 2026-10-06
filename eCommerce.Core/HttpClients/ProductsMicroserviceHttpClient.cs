using eCommerce.Core.DTO;

namespace eCommerce.Core.HttpClients;

public class ProductsMicroserviceHttpClient
{
    private readonly HttpClient _httpClient;
    public ProductsMicroserviceHttpClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ProductDTO?> GetProductByIdAsync(int productId)
    {
        var response = await _httpClient.GetAsync($"search/{productId}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<ProductDTO>();
    }

    public async Task<bool> IsProductExistsAsync(int productId)
    {
        var product = await GetProductByIdAsync(productId);
        return product != null;
    }
} 