using Azure.Identity;
using Azure.Messaging.ServiceBus;

namespace ServiceBus.Demo.Topic.Receiver2
{
    internal class Program
    {
        static async Task Main(string[] args) {
            using CancellationTokenSource cancellationSource = new();
            cancellationSource.CancelAfter(TimeSpan.FromSeconds(45));

            async Task processMessageHandler(ProcessMessageEventArgs args) {
                string body = args.Message.Body.ToString();
                Console.WriteLine($"Received: {body}");
                await args.CompleteMessageAsync(args.Message, cancellationSource.Token);
            }

            Task processErrorHandler(ProcessErrorEventArgs args) => Task.CompletedTask;

            ServiceBusClient client = new("sb-verdel-az204.servicebus.windows.net", new DefaultAzureCredential());
            ServiceBusProcessor processor = client.CreateProcessor("topic-verdel-az204", "S2", new ServiceBusProcessorOptions());

            processor.ProcessMessageAsync += processMessageHandler;
            processor.ProcessErrorAsync += processErrorHandler;

            await processor.StartProcessingAsync(cancellationSource.Token);

            try {
                await Task.Delay(Timeout.Infinite, cancellationSource.Token);
            } catch ( TaskCanceledException ) { }

            try {
                await processor.StopProcessingAsync();
            } finally {
                await processor.DisposeAsync();
                await client.DisposeAsync();
            }
        }
    }
}
