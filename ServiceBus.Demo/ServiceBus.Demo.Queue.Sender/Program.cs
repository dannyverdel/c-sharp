using Azure.Messaging.ServiceBus;

namespace ServiceBus.Demo.Queue.Sender
{
    internal class Program
    {
        private static string _connectionString = "connectionString";
        private static string _queueName = "az204-queue";
        static async Task Main(string[] args) {
            ServiceBusClient client = new(_connectionString);
            ServiceBusSender sender = client.CreateSender(_queueName);

            using ServiceBusMessageBatch messageBatch = await sender.CreateMessageBatchAsync();

            for ( int i = 1; i <= 3; i++ ) {
                if ( !messageBatch.TryAddMessage(new ServiceBusMessage($"Message {i}")) ) {
                    throw new Exception($"Exception {i} has occurred.");
                }
            }

            try {
                await sender.SendMessagesAsync(messageBatch);
                Console.WriteLine($"A batch of three messages has been published to the queue.");
            } finally {
                await sender.DisposeAsync();
                await client.DisposeAsync();
            }

            Console.WriteLine("Follow the directions in the exercise to review the results in the Azure portal.");
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }
    }
}
