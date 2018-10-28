using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Polly;
using Polly.Fallback;
using Polly.Wrap;

namespace xamarin.game.HttpClients
{
    /// <summary>
    /// HttpClient wrapper that integrates Retry and Circuit
    /// breaker policies when invoking HTTP services. 
    /// Based on Polly library: https://github.com/App-vNext/Polly
    /// </summary>
    public class ResilientHttpClient : IHttpClient
    {
        private readonly HttpClient _client;

        public ResilientHttpClient()
        {
            _client = new HttpClient();
        }

        public async Task<string> GetStringAsync(string uri, string authorizationToken = null,
            string authorizationMethod = "Bearer")
        {
            PolicyWrap<string> policyWrap = Policies.WaitAndRetryPolicy
                .WrapAsync(Policies.CircuitBreakerPolicy)
                .WrapAsync(Policies.FallbackForCircuitBreaker)
                .WrapAsync(Policies.FallbackForAnyException);

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            if (authorizationToken != null)
            {
                requestMessage.Headers.Authorization =
                    new AuthenticationHeaderValue(authorizationMethod, authorizationToken);
            }
            
            async Task<string> Action()
            {
                var response = await _client.SendAsync(requestMessage);
                return await response.Content.ReadAsStringAsync();
            }

            return await policyWrap.ExecuteAsync(Action);
        }
    }
}