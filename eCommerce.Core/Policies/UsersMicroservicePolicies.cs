using Microsoft.Extensions.Logging;
using Polly;

namespace eCommerce.Core.Policies;

public class UsersMicroservicePolicies : IUsersMicroservicePolicies
{
    private readonly ILogger<UsersMicroservicePolicies> _logger;

    public UsersMicroservicePolicies(ILogger<UsersMicroservicePolicies> logger)
    {
        _logger = logger;
    }

    public IAsyncPolicy<HttpResponseMessage> GetUsersMicroserviceCircuitBrakerPolicy()
    {
        return Policy.HandleResult<HttpResponseMessage>(t => !t.IsSuccessStatusCode).CircuitBreakerAsync(3, 
            durationOfBreak: TimeSpan.FromSeconds(1),
            onBreak: (outcome, time) => 
            _logger.LogError(""),
            onReset: () => _logger.LogInformation("") 
            );
    }

    public IAsyncPolicy<HttpResponseMessage> GetUsersMicroserviceRetryPolicy()
    {
        return Policy.HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
            .WaitAndRetryAsync(
            3,
            retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), 
            (outcome, timespan, retryCount, context) =>
            _logger.LogWarning("Retrying request after {TimeSpan} (attempt {RetryCount})", timespan, retryCount)
            );
    }

    public IAsyncPolicy<HttpResponseMessage> GetUsersMicroserviceTimeOutPolicy()
    {
        return Policy.TimeoutAsync<HttpResponseMessage>(10);
    }

    public IAsyncPolicy<HttpResponseMessage> GetCombinedPolicyAsync()
    {
        var retryPolicy = GetUsersMicroserviceRetryPolicy();
        var timeOutPolicy = GetUsersMicroserviceTimeOutPolicy();
        var circuitBrakerPolicy = GetUsersMicroserviceCircuitBrakerPolicy();

        return Policy.WrapAsync(retryPolicy, timeOutPolicy, circuitBrakerPolicy);
    }
} 