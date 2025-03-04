using Azure.Messaging.ServiceBus;

namespace ServiceBus.Demo.Queue.Receiver
{
    internal class Program
    {
        private static string _connectionString = "connectionString";
        private static string _queueName = "az204-queue";

        static async Task Main(string[] args) {
            using CancellationTokenSource cancellationSource = new();
            cancellationSource.CancelAfter(TimeSpan.FromSeconds(45));

            async Task processMessageHandler(ProcessMessageEventArgs args) {
                string body = args.Message.Body.ToString();
                Console.WriteLine($"Received: {body}");
                await args.CompleteMessageAsync(args.Message, cancellationSource.Token);
            }

            Task processErrorHandler(ProcessErrorEventArgs args) => Task.CompletedTask;

            ServiceBusClient client = new(_connectionString);
            ServiceBusProcessor processor = client.CreateProcessor(_queueName, new ServiceBusProcessorOptions());

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
