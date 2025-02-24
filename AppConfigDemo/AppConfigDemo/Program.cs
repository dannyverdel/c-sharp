using Microsoft.Extensions.Configuration;
using Microsoft.FeatureManagement;

namespace AppConfigDemo
{
    internal class Program
    {
        static async Task Main(string[] args) {
            var builder = new ConfigurationBuilder();

            builder.AddAzureAppConfiguration(options => {
                options.Connect("MyConnectionString").UseFeatureFlags();
            });

            var config = builder.Build();

            Console.WriteLine(config["TestApp:Settings:Message"] ?? "Hello world!");

            var featureManager = new FeatureManager(new ConfigurationFeatureDefinitionProvider(config));

            // Feature flag
            if ( await featureManager.IsEnabledAsync("Beta") ) {
                Console.WriteLine("Beta feature is enabled");
            } else {
                Console.WriteLine("Beta feature is disabled");
            }

            // Variant feature flag
            if ( await featureManager.IsEnabledAsync("Greeting") ) {
                Console.WriteLine("Variant feature is enabled");
                Variant greetingVariant = await featureManager.GetVariantAsync("Greeting")!;
                Console.WriteLine($"You are working with the feature variant {greetingVariant.Name}");

            } else {
                Console.WriteLine("Variant feature is disabled");
            }
        }
    }
}
