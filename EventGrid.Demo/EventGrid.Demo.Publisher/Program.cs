using Azure.Messaging;
using Azure.Messaging.EventGrid.Namespaces;

namespace EventGrid.Demo.Publisher
{
    internal class Program
    {
        static async Task Main(string[] args) {
            string namespaceEndpoint = "https://egn-verdel-az204.westeurope-1.eventgrid.azure.net";
            string topicName = "egt-verdel-az204";
            string topicKey = "2p2uFuWmj1A58GAIu3Cg7UAAzSKdwh3wr4QWxExtgBugdiyPlMCGJQQJ99BCAC5RqLJXJ3w3AAABAZEGxM8c";

            EventGridSenderClient client = new(new Uri(namespaceEndpoint), topicName, new Azure.AzureKeyCredential(topicKey));

            CloudEvent cloudEvent = new("employee_source", "type", new TestModel() { Name = "Danny", Age = 22 });
            await client.SendAsync(cloudEvent);

            await client.SendAsync(new List<CloudEvent>() {
                new("employee_source", "type", new TestModel() { Name = "Famke", Age = 20 }),
                new("employee_source", "type", new TestModel() { Name = "Tim", Age = 19 })
            });

            Console.WriteLine("Three events have been published to the topic. Press any key to end the application.");
            Console.ReadKey();
        }
    }

    public class TestModel
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
