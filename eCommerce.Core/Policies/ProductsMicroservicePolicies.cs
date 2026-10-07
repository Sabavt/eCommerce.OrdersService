using DnsClient.Internal;
using eCommerce.Core.DTO;
using Polly;
using System.Text.Json;

namespace eCommerce.Core.Policies;

public class ProductsMicroservicePolicies(ILogger logger) : IProductsMicroservicePolicies
{
    private readonly ILogger _logger = logger;

    public IAsyncPolicy<HttpResponseMessage> GetProductsMicroserviceFallbackPolicy()
    {
        return Policy<HttpResponseMessage>.HandleResult(t => !t.IsSuccessStatusCode).FallbackAsync(async(context) =>
        {
            _logger.LogWarning("Fallback has been triggered, request has been failed, returning dummy value!");

            ProductDTO product = new ProductDTO()
            {
                Category = "None",
                Price = 1,
                ProductName = "None",
                ProductDesciption = "None",
                Quantity = 1
            };
            return new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = new StringContent(JsonSerializer.Serialize(product), System.Text.Encoding.UTF8, "application/json") 
            };
        });
    }
}