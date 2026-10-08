using eCommerce.Core.DTO;
using Microsoft.Extensions.Logging;
using Polly.Bulkhead;
using System.Net.Http.Json;

namespace eCommerce.Core.HttpClients;

public class ProductsMicroserviceHttpClient(ILogger<ProductsMicroserviceHttpClient> logger, HttpClient httpClient)
{
    private readonly ILogger<ProductsMicroserviceHttpClient> _logger = logger;
    private readonly HttpClient _httpClient = httpClient;

    public async Task<ProductDTO?> GetProductByIdAsync(int productId)
    {
        try
        {
            var response = await _httpClient.GetAsync($"search/{productId}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ProductDTO>();
        }
        catch (BulkheadRejectedException ex)
        {
            _logger.LogWarning(ex,"BulkHeadRejectedException occured during executing GetProductByIdAsync method.");

            return null;
        }
    }

    public async Task<bool> IsProductExistsAsync(int productId)
    {
        var product = await GetProductByIdAsync(productId);
        return product != null;
    }
} 