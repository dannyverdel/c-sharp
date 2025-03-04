using Azure.Identity;
using Azure.Messaging.ServiceBus;

namespace ServiceBus.Demo.Topic.Sender
{
    internal class Program
    {
        private static readonly int _numberOfMessages = 10;
        static async Task Main(string[] args) {
            ServiceBusClient client = new("sb-verdel-az204.servicebus.windows.net", new DefaultAzureCredential());
            ServiceBusSender sender = client.CreateSender("topic-verdel-az204");

            using ServiceBusMessageBatch messageBatch = await sender.CreateMessageBatchAsync();

            for ( int i = 1; i <= _numberOfMessages; i++ ) {
                if ( !messageBatch.TryAddMessage(new ServiceBusMessage($"Message {i}")) ) {
                    throw new Exception($"The message {i} is too large to fit in the batch.");
                }
            }

            try {
                // Use the producer client to send the batch of messages to the Service Bus topic
                await sender.SendMessagesAsync(messageBatch);
                Console.WriteLine($"A batch of {_numberOfMessages} messages has been published to the topic.");
            } finally {
                // Calling DisposeAsync on client types is required to ensure that network
                // resources and other unmanaged objects are properly cleaned up.
                await sender.DisposeAsync();
                await client.DisposeAsync();
            }

            Console.WriteLine("Press any key to end the application");
            Console.ReadKey();
        }
    }
}
