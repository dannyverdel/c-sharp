using Azure.Identity;
using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;

namespace AzureStorageQueue.Demo.Receiver
{
    internal class Program
    {
        private static string _queueName = "customerrequest";
        private static string _storageAccountName = "saverdelaz204";

        static void Main(string[] args) {
            QueueClient queueClient = new(new Uri($"https://{_storageAccountName}.queue.core.windows.net/{_queueName}"), new DefaultAzureCredential());

            PeekedMessage[] peekedMessages = queueClient.PeekMessages(maxMessages: 10);

            foreach ( PeekedMessage message in peekedMessages ) {
                Console.WriteLine($"Message: {message.MessageText}");
            }

            QueueMessage[] messages = queueClient.ReceiveMessages(maxMessages: 10);

            foreach ( QueueMessage message in messages ) {
                Console.WriteLine($"Message: {message.MessageText}");
                queueClient.DeleteMessage(message.MessageId, message.PopReceipt);
            }
        }
    }
}
