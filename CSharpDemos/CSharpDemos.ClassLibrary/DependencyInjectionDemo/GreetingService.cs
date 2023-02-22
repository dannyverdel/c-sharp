using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CSharpDemos.ClassLibrary.DependencyInjectionDemo;

public class GreetingService : IGreetingService
{
	private readonly ILogger<GreetingService> _log;
	private readonly IConfiguration _config;

	public GreetingService(ILogger<GreetingService> log, IConfiguration config)
	{
		_log = log;
		_config = config;
	}

	public void Run()
	{
		_log.LogInformation("Fetched from Serilog.MinimumLevel.Override.Microsoft: {SerilogMinimumLevelOverrideMicrosoft}", _config.GetValue<string>("Serilog:MinimumLevel:Override:Microsoft"));
		for (int i = 0; i < _config.GetValue<int>("LoopTimes"); i++)
			_log.LogInformation("Run number {runnumber}", i);
	}
}

