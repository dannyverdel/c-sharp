using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Consumer;
using Azure.Messaging.EventHubs.Processor;
using Azure.Storage.Blobs;
using System.Text;

namespace EventHub.Demo.Consumer
{
    internal class Program
    {
        private readonly static string _connectionString = "myConnectionString";
        private readonly static string _eventHubName = "eventHubName";
        private readonly static string _consumerGroup = EventHubConsumerClient.DefaultConsumerGroupName;
        private readonly static string _storageConnectionString = "myConnectionString";
        private readonly static string _blobContainerName = "events";

        static async Task Main(string[] args) {
            await ReadEvents();
            //await ProcessEventsWithCheckpoint();
        }

        static async Task ReadEvents() {
            await using ( EventHubConsumerClient consumer = new(_consumerGroup, _connectionString, _eventHubName) ) {
                using CancellationTokenSource cancellationSource = new();
                cancellationSource.CancelAfter(TimeSpan.FromSeconds(45));

                await foreach ( PartitionEvent receivedEvent in consumer.ReadEventsAsync(false, null, cancellationSource.Token) ) {
                    Console.WriteLine($"Event Received: {Encoding.UTF8.GetString(receivedEvent.Data.Body.ToArray())}");
                }
            }
        }

        static async Task ProcessEventsWithCheckpoint() {
            using CancellationTokenSource cancellationSource = new();
            cancellationSource.CancelAfter(TimeSpan.FromSeconds(45));

            Task processEventHandler(ProcessEventArgs eventArgs) {
                Console.WriteLine($"Event Received: {Encoding.UTF8.GetString(eventArgs.Data.Body.ToArray())}");
                eventArgs.UpdateCheckpointAsync(eventArgs.CancellationToken);
                return Task.CompletedTask;
            }

            Task processErrorHandler(ProcessErrorEventArgs eventArgs) => Task.CompletedTask;

            BlobContainerClient storageClient = new(_storageConnectionString, _blobContainerName);
            EventProcessorClient processor = new(storageClient, _consumerGroup, _connectionString, _eventHubName);

            processor.ProcessEventAsync += processEventHandler;
            processor.ProcessErrorAsync += processErrorHandler;

            await processor.StartProcessingAsync(cancellationSource.Token);

            try {
                await Task.Delay(Timeout.Infinite, cancellationSource.Token);
            } catch ( TaskCanceledException ) {
                // expcected when the delay is canceled
            }

            try {
                await processor.StopProcessingAsync();
            } finally {
                processor.ProcessEventAsync -= processEventHandler;
                processor.ProcessErrorAsync -= processErrorHandler;
            }
        }
    }
}
