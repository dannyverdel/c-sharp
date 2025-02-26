using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;

namespace AppConfigDemo.Function
{
    public class Function1
    {
        private readonly ILogger<Function1> _logger;
        private readonly IConfiguration _configuration;
        private readonly IConfigurationRefresher _configurationRefresher;

        public Function1(ILogger<Function1> logger, IConfiguration configuration, IConfigurationRefresherProvider configurationRefresherProvider) {
            _logger = logger;
            _configuration = configuration;
            _configurationRefresher = configurationRefresherProvider.Refreshers.First();
        }

        [Function("Function1")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req) {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            await _configurationRefresher.TryRefreshAsync();

            // Simpele configurabele ophalen
            string loggingLevel = _configuration["LoggingLevel"] ?? "Geen LoggingLevel gevonden";
            // Configurabele die zich eigenlijk in de key vault bevindt ophalen
            string redisConnectionString = _configuration["REDIS-CONNECTIONSTRING"] ?? "Geen connectionstring gevonden";

            // Feature manager aanmaken
            var featureManager = new FeatureManager(new ConfigurationFeatureDefinitionProvider(_configuration));

            // Feature flag genaamd beta die beheerd wordt in Azure App Configuration
            string alphaFeature = await featureManager.IsEnabledAsync("Alpha") ? "Alpha feature is enabled" : "Alpha feature is disabled";

            // Variant feature flag genaamd Beta die beheerd wordt in Azure App Configuration waarbij 20% krijgt de 'On' variant en 80% de 'Off' variant. Deze verdeling is ook te beheren.
            string betaFeature = await featureManager.IsEnabledAsync("Beta") && ( await featureManager.GetVariantAsync("Beta") ).Name == "On" ? "Variant feature Beta is enabled" : "Variant feature is disabled";

            return new OkObjectResult(new {
                loggingLevel,
                redisConnectionString,
                alphaFeature,
                betaFeature
            });
        }
    }
}
