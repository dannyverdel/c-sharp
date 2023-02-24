// See https://aka.ms/new-console-template for more information
using CSharpDemos.ClassLibrary;
using CSharpDemos.ClassLibrary.DependencyInjectionDemo;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace CSharpDemos.DemoApp;

class Program
{
    static void Main(string[] args) {
        ConfigurationBuilder builder = new ConfigurationBuilder();
        BuildConfig(builder);

        Log.Logger = new LoggerConfiguration()
          .ReadFrom.Configuration(builder.Build())
          .Enrich.FromLogContext()
          .WriteTo.Console()
          .CreateLogger();

        Log.Logger.Information("Application Starting");

        IHost host = Host.CreateDefaultBuilder()
          .ConfigureServices((context, services) => {
              services.AddTransient<IGreetingService, GreetingService>();
              services.AddTransient<IInvokeMethod, CSharpDemos.ClassLibrary.DesignPatterns.RepositoryPattern.InvokeRepositoryPattern>();
          })
          .UseSerilog()
          .Build();

        var svc = ActivatorUtilities.CreateInstance<CSharpDemos.ClassLibrary.DesignPatterns.RepositoryPattern.InvokeRepositoryPattern>(host.Services);
        svc.InvokeMethod();

        Console.ReadLine();
    }

    static void BuildConfig(IConfigurationBuilder builder) {
        builder.SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
          .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
          .AddEnvironmentVariables();
    }
}