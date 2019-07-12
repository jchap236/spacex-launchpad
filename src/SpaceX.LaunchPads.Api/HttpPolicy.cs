using System;
using System.Net.Http;
using Polly;
using Polly.Extensions.Http;

namespace SpaceX.LaunchPads.Api
{
    public static class HttpPolicy
    {
        public static IAsyncPolicy<HttpResponseMessage> NewRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        }
    }
}