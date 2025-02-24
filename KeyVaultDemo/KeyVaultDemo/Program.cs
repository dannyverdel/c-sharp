using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

namespace KeyVaultDemo
{
    internal class Program
    {
        static async Task Main(string[] args) {
            string keyVaultUir = "myKeyVaultUri";

            SecretClient secretClient = new(new Uri(keyVaultUir), new DefaultAzureCredential());

            await secretClient.SetSecretAsync("connectionString", "myConnectionString");

            KeyVaultSecret secret = await secretClient.GetSecretAsync("connectionString");
            Console.WriteLine(secret.Name + ": " + secret.Value);

            DeleteSecretOperation? deleteSecretOperation = await secretClient.StartDeleteSecretAsync("connectionString");
            await deleteSecretOperation.WaitForCompletionAsync();
            await secretClient.PurgeDeletedSecretAsync("connectionString");
        }
    }
}
