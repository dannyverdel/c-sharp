using Azure.Messaging;
using Azure.Messaging.EventGrid.Namespaces;

namespace EventGrid.Demo.Subscriber
{
    internal class Program
    {
        static async Task Main(string[] args) {
            string namespaceEndpoint = "https://egn-verdel-az204.westeurope-1.eventgrid.azure.net";
            string topicName = "egt-verdel-az204";
            string topicKey = "2p2uFuWmj1A58GAIu3Cg7UAAzSKdwh3wr4QWxExtgBugdiyPlMCGJQQJ99BCAC5RqLJXJ3w3AAABAZEGxM8c";
            string subscriptionName = "egt-sub-verdel-az204";
            const short maxEventCount = 3;

            EventGridReceiverClient client = new(new Uri(namespaceEndpoint), topicName, subscriptionName, new Azure.AzureKeyCredential(topicKey));

            ReceiveResult result = await client.ReceiveAsync(maxEventCount);

            Console.WriteLine("Received Response");
            Console.WriteLine("-----------------");

            List<string> toRelease = new();
            List<string> toAcknowledge = new();
            List<string> toReject = new();

            foreach ( ReceiveDetails detail in result.Details ) {
                CloudEvent cloudEvent = detail.Event;
                BrokerProperties brokerProperties = detail.BrokerProperties;
                Console.WriteLine(cloudEvent.Data.ToString());

                Console.WriteLine(brokerProperties.LockToken);
                Console.WriteLine();

                // If the event is from the "employee_source" and the name is "Tim", we are not able to acknowledge it yet, so we release it
                if ( cloudEvent.Source == "employee_source" && cloudEvent.Data.ToObjectFromJson<TestModel>().Name == "Tim" ) {
                    toRelease.Add(brokerProperties.LockToken);
                }
                // acknowledge other employee_source events
                else if ( cloudEvent.Source == "employee_source" ) {
                    toAcknowledge.Add(brokerProperties.LockToken);
                }
                // reject all other events
                else {
                    toReject.Add(brokerProperties.LockToken);
                }
            }

            if ( toRelease.Count > 0 ) {
                ReleaseResult releaseResult = await client.ReleaseAsync(toRelease);

                // Inspect the Release result
                Console.WriteLine($"Failed count for Release: {releaseResult.FailedLockTokens.Count}");
                foreach ( FailedLockToken failedLockToken in releaseResult.FailedLockTokens ) {
                    Console.WriteLine($"Lock Token: {failedLockToken.LockToken}");
                    Console.WriteLine($"Error Code: {failedLockToken.Error}");
                    Console.WriteLine($"Error Description: {failedLockToken.ToString}");
                }

                Console.WriteLine($"Success count for Release: {releaseResult.SucceededLockTokens.Count}");
                foreach ( string lockToken in releaseResult.SucceededLockTokens ) {
                    Console.WriteLine($"Lock Token: {lockToken}");
                }
                Console.WriteLine();
            }

            if ( toAcknowledge.Count > 0 ) {
                AcknowledgeResult acknowledgeResult = await client.AcknowledgeAsync(toAcknowledge);

                // Inspect the Acknowledge result
                Console.WriteLine($"Failed count for Acknowledge: {acknowledgeResult.FailedLockTokens.Count}");
                foreach ( FailedLockToken failedLockToken in acknowledgeResult.FailedLockTokens ) {
                    Console.WriteLine($"Lock Token: {failedLockToken.LockToken}");
                    Console.WriteLine($"Error Code: {failedLockToken.Error}");
                    Console.WriteLine($"Error Description: {failedLockToken.ToString}");
                }

                Console.WriteLine($"Success count for Acknowledge: {acknowledgeResult.SucceededLockTokens.Count}");
                foreach ( string lockToken in acknowledgeResult.SucceededLockTokens ) {
                    Console.WriteLine($"Lock Token: {lockToken}");
                }
                Console.WriteLine();
            }

            if ( toReject.Count > 0 ) {
                RejectResult rejectResult = await client.RejectAsync(toReject);

                // Inspect the Reject result
                Console.WriteLine($"Failed count for Reject: {rejectResult.FailedLockTokens.Count}");
                foreach ( FailedLockToken failedLockToken in rejectResult.FailedLockTokens ) {
                    Console.WriteLine($"Lock Token: {failedLockToken.LockToken}");
                    Console.WriteLine($"Error Code: {failedLockToken.Error}");
                    Console.WriteLine($"Error Description: {failedLockToken.ToString}");
                }

                Console.WriteLine($"Success count for Reject: {rejectResult.SucceededLockTokens.Count}");
                foreach ( string lockToken in rejectResult.SucceededLockTokens ) {
                    Console.WriteLine($"Lock Token: {lockToken}");
                }
                Console.WriteLine();
            }
        }
    }

    public class TestModel
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
