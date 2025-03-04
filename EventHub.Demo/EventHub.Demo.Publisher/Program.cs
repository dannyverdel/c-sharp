using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;

namespace EventHub.Demo.Publisher
{
    internal class Program
    {
        static async Task Main(string[] args) {
            string connectionString = "myConnectionString";
            string eventHubName = "eventHubName";

            await using ( EventHubProducerClient producer = new(connectionString, eventHubName) ) {
                string[] partitionIds = await producer.GetPartitionIdsAsync();
                foreach ( string partitionId in partitionIds ) {
                    Console.WriteLine(partitionId);
                }

                using EventDataBatch eventBatch = await producer.CreateBatchAsync();
                eventBatch.TryAdd(new EventData(new BinaryData(new { Name = "Test 1", Age = 22 })));
                eventBatch.TryAdd(new EventData(new BinaryData(new { Name = "Test 2", Age = 22 })));

                await producer.SendAsync(eventBatch,);
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
