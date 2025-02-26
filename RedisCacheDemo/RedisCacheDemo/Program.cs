using StackExchange.Redis;
using System.Text.Json;

namespace RedisCacheDemo
{
    internal class Program
    {
        private static string _connectionString = "myConnectionString";
        static async Task Main(string[] args) {
            List<int> temperatures = await GetData();
            Console.WriteLine("Temperatures");
        }

        static async Task<List<int>> GetData() {
            ConnectionMultiplexer connectionMultiplexer = await ConnectionMultiplexer.ConnectAsync(_connectionString);
            IDatabase database = connectionMultiplexer.GetDatabase();

            const string cacheKey = "temperatures";

            if ( database.KeyExists(cacheKey) ) {
                List<int>? result = JsonSerializer.Deserialize<List<int>>(database.StringGet(cacheKey));
                if ( result is not null ) {
                    Console.WriteLine("Returning from cache");
                    return result;
                }
            }

            List<int> temperatures = new List<int> { 20, 21, 22, 23, 24, 25, 26, 27, 28, 29 };
            database.StringSet(cacheKey, JsonSerializer.Serialize(temperatures), DateTime.Now.AddMinutes(10).Subtract(DateTime.Now));

            Console.WriteLine("Returning from get data");

            connectionMultiplexer.Dispose();
            connectionMultiplexer = null;
            return temperatures;
        }
    }
}