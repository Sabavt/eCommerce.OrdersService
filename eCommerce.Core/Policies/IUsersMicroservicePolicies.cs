using Polly;

namespace eCommerce.Core.Policies;

public interface IUsersMicroservicePolicies
{
    public IAsyncPolicy<HttpResponseMessage> GetUsersMicroserviceRetryPolicy();
    public IAsyncPolicy<HttpResponseMessage> GetUsersMicroserviceCircuitBrakerPolicy();
} 