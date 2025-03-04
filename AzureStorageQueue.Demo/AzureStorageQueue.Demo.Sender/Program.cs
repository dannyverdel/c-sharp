using Azure.Identity;
using Azure.Storage.Queues;
using System.Text.Json;

namespace AzureStorageQueue.Demo.Sender
{
    internal class Program
    {
        private static string _queueName = "customerrequest";
        private static string _storageAccountName = "saverdelaz204";
        static async Task Main(string[] args) {
            QueueClient queueClient = new(new Uri($"https://{_storageAccountName}.queue.core.windows.net/{_queueName}"), new DefaultAzureCredential());

            object data = new {
                Name = "Danny",
                Age = 22
            };

            string message = JsonSerializer.Serialize(data);
            await queueClient.SendMessageAsync(message);
        }
    }
}
