using DnsClient.Internal;
using eCommerce.Core.DTO;
using Polly;
using Polly.Bulkhead;
using System.Text.Json;

namespace eCommerce.Core.Policies;

public class ProductsMicroservicePolicies(ILogger logger) : IProductsMicroservicePolicies
{
    private readonly ILogger _logger = logger;

    public IAsyncPolicy<HttpResponseMessage> GetProductsMicroserviceBulkheadIsolationPolicy()
    {
        return Policy.BulkheadAsync<HttpResponseMessage>(4, 30, (context) => {
            _logger.LogWarning("Bulkhead Isolation triggered. Can't  send any more reuests, because queue is full");

            throw new BulkheadRejectedException("Queue is full.");
        });
    }

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

    public IAsyncPolicy<HttpResponseMessage> GetProductsMicroserviceTimeoutPolicy()
    {
        return Policy.TimeoutAsync<HttpResponseMessage>(10);
    }

    public IAsyncPolicy<HttpResponseMessage> GetCombinedPolicyAsync()
    {
        var timeOutPolicy = GetProductsMicroserviceTimeoutPolicy();
        var fallBackPolicy = GetProductsMicroserviceFallbackPolicy();
        var bulkHeadPolicy = GetProductsMicroserviceBulkheadIsolationPolicy();

       return Policy.WrapAsync(timeOutPolicy,  fallBackPolicy, bulkHeadPolicy);
    }
}