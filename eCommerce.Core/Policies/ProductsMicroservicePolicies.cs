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
       
    }

    public IAsyncPolicy<HttpResponseMessage> GetProductsMicroserviceFallbackPolicy()
    {
        
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