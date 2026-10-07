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

    public IAsyncPolicy<HttpResponseMessage> GetUsersMicroserviceRetryPolicy()
    {
        return Policy.HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode).WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (outcome, timespan, retryCount, context) => _logger.LogWarning("Retrying request after {TimeSpan} (attempt {RetryCount})", timespan, retryCount));
    }
}
