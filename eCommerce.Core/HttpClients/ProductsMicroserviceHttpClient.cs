using eCommerce.Core.DTO;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Polly.Bulkhead;
using System.Net.Http.Json;
using System.Text.Json;

namespace eCommerce.Core.HttpClients;

public class ProductsMicroserviceHttpClient(ILogger<ProductsMicroserviceHttpClient> logger, HttpClient httpClient, IDistributedCache distributedCache)
{
    private readonly ILogger<ProductsMicroserviceHttpClient> _logger = logger;
    private readonly HttpClient _httpClient = httpClient;
    private readonly IDistributedCache _distributedCache = distributedCache;

    public async Task<ProductDTO?> GetProductByIdAsync(int productId)
    {
        try
        { 
            string? cachedProduct = await _distributedCache.GetStringAsync($"product:{productId}");
            if (cachedProduct != null)
            {
                var product = JsonSerializer.Deserialize<ProductDTO>(cachedProduct);
                return product;
            }
            var response = await _httpClient.GetAsync(requestUri: $"search/{productId}");
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