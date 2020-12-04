using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;

namespace MarcDuiker.Automation.Orchestrators
{
    public class ScrapingOrchestrator
    {
        [FunctionName(nameof(ScrapingOrchestrator))]
        public async Task Run(
            [OrchestrationTrigger] IDurableOrchestrationContext context,
            ILogger logger)
        {
            var uri = context.GetInput<Uri>();
            var response  = await context.CallHttpAsync(HttpMethod.Get, uri);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                logger.LogError(response.StatusCode.ToString());
            };
        }
    }
}