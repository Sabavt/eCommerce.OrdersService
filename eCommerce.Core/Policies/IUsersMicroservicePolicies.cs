using Polly;

namespace eCommerce.Core.Policies;

public interface IUsersMicroservicePolicies
{
    IAsyncPolicy<HttpResponseMessage> GetUsersMicroserviceRetryPolicy();
    IAsyncPolicy<HttpResponseMessage> GetUsersMicroserviceCircuitBrakerPolicy();
    IAsyncPolicy<HttpResponseMessage> GetUsersMicroserviceTimeOutPolicy();
    IAsyncPolicy<HttpResponseMessage> GetCombinedPolicyAsync(); 
}