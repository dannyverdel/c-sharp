using Azure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.FeatureManagement;

namespace AppConfigDemo
{
    internal class Program
    {
        static async Task Main(string[] args) {
            // https://learn.microsoft.com/en-us/azure/azure-app-configuration/enable-dynamic-configuration-azure-functions-csharp?tabs=in-process 
            var builder = new ConfigurationBuilder();
            IConfigurationRefresher? refresher = null;

            // Maak connectie met Azure App Configuration
            builder.AddAzureAppConfiguration(options => {
                options.Connect("myConnectionString")
                .ConfigureKeyVault(kv => kv.SetCredential(new DefaultAzureCredential()))
                .Select(KeyFilter.Any, LabelFilter.Null)
                .Select(KeyFilter.Any, "Development")
                .UseFeatureFlags()
                .ConfigureRefresh(refresh => {
                    // Maak veranderingen in je Azure App Configuration en zie dat deze binnen 1 seconde worden opgehaald nadat de refreshSentinel key is aangepast
                    refresh.Register("refreshSentinel", LabelFilter.Null, refreshAll: true)
                    .SetRefreshInterval(TimeSpan.FromSeconds(1));
                });

                refresher = options.GetRefresher();
            });

            var config = builder.Build();

            while ( true ) {
                if ( refresher is not null ) {
                    await refresher.TryRefreshAsync();
                } else {
                    Console.WriteLine("Warning: Couldn't refresh App Configuration");
                }
                // Simpele configurabele ophalen
                Console.WriteLine(config["LoggingLevel"] ?? "Hello world!");
                // Configurabele die zich eigenlijk in de key vault bevindt ophalen
                Console.WriteLine(config["REDIS-CONNECTIONSTRING"] ?? "Geen connectionstring gevonden");

                // Feature manager aanmaken
                var featureManager = new FeatureManager(new ConfigurationFeatureDefinitionProvider(config));

                // Feature flag genaamd beta die beheerd wordt in Azure App Configuration
                if ( await featureManager.IsEnabledAsync("Alpha") ) {
                    Console.WriteLine("Alpha feature is enabled");
                } else {
                    Console.WriteLine("Alpha feature is disabled");
                }

                // Variant feature flag genaamd Beta die beheerd wordt in Azure App Configuration waarbij 20% krijgt de 'On' variant en 80% de 'Off' variant. Deze verdeling is ook te beheren.
                if ( await featureManager.IsEnabledAsync("Beta") && featureManager.GetVariantAsync("Beta").GetAwaiter().GetResult().Name == "On" ) {
                    Console.WriteLine("Variant feature Beta is enabled");

                } else {
                    Console.WriteLine("Variant feature is disabled");
                }

                if ( Console.ReadLine() == "exit" ) {
                    break;
                }
            }
        }
    }
}
