using Azure.Identity;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.Extensions.Hosting;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

// Application Insights isn't enabled by default. See https://aka.ms/AAt8mw4.
// builder.Services
//     .AddApplicationInsightsTelemetryWorkerService()
//     .ConfigureFunctionsApplicationInsights();

builder.Configuration.AddAzureAppConfiguration(options => {
    options.Connect("myConnectionString")
    .ConfigureKeyVault(kv => kv.SetCredential(new DefaultAzureCredential()))
    .Select(KeyFilter.Any, LabelFilter.Null)
    .Select(KeyFilter.Any, "Development")
    .UseFeatureFlags()
    .ConfigureRefresh(refresh => {
        // Maak veranderingen in je Azure App Configuration en zie dat deze binnen 1 seconde worden opgehaald nadat de refreshSentinel key is aangepast
        refresh.Register("refreshSentinel", LabelFilter.Null, refreshAll: true)
        .SetRefreshInterval(TimeSpan.FromSeconds(10));
    });
});

builder.Services.AddAzureAppConfiguration();

builder.Build().Run();
