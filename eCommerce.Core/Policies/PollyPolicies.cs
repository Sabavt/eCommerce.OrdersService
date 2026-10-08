using Microsoft.Extensions.Logging;
using Polly;
using Polly.Bulkhead; 

namespace eCommerce.Core.Policies;

public class PollyPolicies : IPollyPolicies
{
    private readonly ILogger<UsersMicroservicePolicies> _logger;

    public PollyPolicies(ILogger<UsersMicroservicePolicies> logger)
    {
        _logger = logger;
    }

    public IAsyncPolicy<HttpResponseMessage> GetCircuitBrakerPolicy(int handledEventsAllowedBeforeBreaking, TimeSpan durationOfBreak)
    {
        return Policy.HandleResult<HttpResponseMessage>(t => !t.IsSuccessStatusCode).CircuitBreakerAsync(handledEventsAllowedBeforeBreaking, 
            durationOfBreak,
            onBreak: (outcome, time) => 
            _logger.LogError(""),
            onReset: () => _logger.LogInformation("") 
            );
    }

    public IAsyncPolicy<HttpResponseMessage> GetRetryPolicy(int retryCount)
    {
        return Policy.HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
            .WaitAndRetryAsync(
            retryCount,
            retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), 
            (outcome, timespan, retryCount, context) =>
            _logger.LogWarning("Retrying request after {TimeSpan} (attempt {RetryCount})", timespan, retryCount)
            );
    }

    public IAsyncPolicy<HttpResponseMessage> GetTimeOutPolicy(int seconds)
    {
        return Policy.TimeoutAsync<HttpResponseMessage>(seconds);
    }

    public IAsyncPolicy<HttpResponseMessage> GetCombinedPolicyAsync()
    {
        var retryPolicy = GetRetryPolicy(3);
        var timeOutPolicy = GetTimeOutPolicy(10);
        var circuitBrakerPolicy = GetCircuitBrakerPolicy(2, TimeSpan.FromSeconds(5));

        return Policy.WrapAsync(retryPolicy, timeOutPolicy, circuitBrakerPolicy);
    }

    public IAsyncPolicy<HttpResponseMessage> GetFallbackPolicy()
    {
        return Policy<HttpResponseMessage>.HandleResult(t => !t.IsSuccessStatusCode).FallbackAsync(async (context) =>
        {
            _logger.LogWarning("Fallback has been triggered, request has been failed, returning dummy value!");

            return new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = new StringContent("")
            };
        });
    }

    public IAsyncPolicy<HttpResponseMessage> GetBulkheadIsolationPolicy(int maxParallelization, int maxQueuingActions)
    {
        return Policy.BulkheadAsync<HttpResponseMessage>(maxParallelization, maxQueuingActions, (context) => {
            _logger.LogWarning("Bulkhead Isolation triggered. Can't  send any more reuests, because queue is full");

            throw new BulkheadRejectedException("Queue is full.");
        });
    }
} 