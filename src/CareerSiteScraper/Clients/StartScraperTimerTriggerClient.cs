using System;
using System.Threading.Tasks;
using MarcDuiker.Automation.Orchestrators;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;

namespace CareerSiteScraper.Clients
{
    public class StartScraperTimerTriggerClient
    {
        [FunctionName("StartScraperTimerTriggerClient")]
        public async Task Run(
            [TimerTrigger("%TimerFrequency%")]TimerInfo myTimer,
            [DurableClient] IDurableClient client,
            ILogger log)
        {
            var baseUrl = Environment.GetEnvironmentVariable("UrlToGet");
            var message = Environment.GetEnvironmentVariable("UrlMessage");
            var uri = new Uri($"{baseUrl}&message={message}");
            
            await client.StartNewAsync(
                nameof(ScrapingOrchestrator),
                uri);
        }
    }
}
