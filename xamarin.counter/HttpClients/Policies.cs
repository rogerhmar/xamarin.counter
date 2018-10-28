using Polly;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Polly.CircuitBreaker;
using Polly.Fallback;
using Polly.Wrap;

namespace xamarin.counter.HttpClients
{
    public static class Policies
    {
        public static IAsyncPolicy WaitAndRetryPolicy => Policy
            .Handle<Exception            >(e => !(e is BrokenCircuitException)) // Exception filtering!  We don't retry if the inner circuit-breaker judges the underlying system is out of commission!
            .WaitAndRetryForeverAsync(
                attempt => TimeSpan.FromMilliseconds(200),
                (exception, calculatedWaitDuration) =>
                {
                    ConsoleHelper.WriteLineInColor(".Log,then retry: " + exception.Message, ConsoleColor.Yellow);
                });


        public static IAsyncPolicy CircuitBreakerPolicy => Policy
            .Handle<Exception>()
            .CircuitBreakerAsync(
                exceptionsAllowedBeforeBreaking: 4,
                durationOfBreak: TimeSpan.FromSeconds(3),
                onBreak: (ex, breakDelay) =>
                {
                    ConsoleHelper.WriteLineInColor(
                        ".Breaker logging: Breaking the circuit for " + breakDelay.TotalMilliseconds + "ms!",
                        ConsoleColor.Magenta);
                    ConsoleHelper.WriteLineInColor("..due to: " + ex.Message, ConsoleColor.Magenta);
                },
                onReset: () => ConsoleHelper.WriteLineInColor(".Breaker logging: Call ok! Closed the circuit again!",
                    ConsoleColor.Magenta),
                onHalfOpen: () =>
                    ConsoleHelper.WriteLineInColor(".Breaker logging: Half-open: Next call is a trial!",
                        ConsoleColor.Magenta)
            );

        public static FallbackPolicy<string> FallbackForCircuitBreaker = Policy<string>
            .Handle<BrokenCircuitException>()
            .FallbackAsync(
                fallbackValue: "Please try again later [Fallback for broken circuit]",
                onFallbackAsync: OnFallbackCircutBreakerAsync);

        public static async Task OnFallbackCircutBreakerAsync(DelegateResult<string> b)
        {
            ConsoleHelper.WriteInColor("Fallback catches failed with: " + b.Exception.Message, ConsoleColor.Red);
        }

        public static FallbackPolicy<string> FallbackForAnyException => Policy<string>
            .Handle<Exception>()
            .FallbackAsync(
                fallbackAction: async c => "Please try again later [Fallback for any exception]",
                onFallbackAsync: async e =>
                    ConsoleHelper.WriteInColor("Fallback catches eventually failed with: " + e.Exception.Message,
                        ConsoleColor.Red)
            );
    }
}