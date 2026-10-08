using Polly;

namespace eCommerce.Core.Policies;

public interface IPollyPolicies
{
    IAsyncPolicy<HttpResponseMessage> GetRetryPolicy(int retryCount);
    IAsyncPolicy<HttpResponseMessage> GetCircuitBrakerPolicy(int handledEventsAllowedBeforeBreaking, TimeSpan durationOfBreak);
    IAsyncPolicy<HttpResponseMessage> GetTimeOutPolicy(int seconds); 
    IAsyncPolicy<HttpResponseMessage> GetFallbackPolicy();
    IAsyncPolicy<HttpResponseMessage> GetBulkheadIsolationPolicy(int maxParallelization, int maxQueuingActions);
    IAsyncPolicy<HttpResponseMessage> GetCombinedPolicyAsync();
}