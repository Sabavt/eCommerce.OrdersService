using Polly;

namespace eCommerce.Core.Policies;

public interface IProductsMicroservicePolicies
{
    IAsyncPolicy<HttpResponseMessage> GetProductsMicroserviceFallbackPolicy();
    IAsyncPolicy<HttpResponseMessage> GetProductsMicroserviceTimeoutPolicy();
    IAsyncPolicy<HttpResponseMessage> GetProductsMicroserviceBulkheadIsolationPolicy();
    IAsyncPolicy<HttpResponseMessage> GetCombinedPolicyAsync();
} 