using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace LocatorApp.Data
{
    internal class DatabaseComunication
    {
        private static readonly HttpClient client = new HttpClient();

        public static async Task<string> getData(string deviceId)
        {
            if (string.IsNullOrEmpty(deviceId))
            {
                Console.WriteLine("Error: Device ID is null or empty.");
                return "{}"; // Return empty JSON object
            }

            string url = $"https://biocesta.sk/oliver/stahovanie.php?id={deviceId}";
            return await FetchJsonFromApi(url);
        }

        private static async Task<string> FetchJsonFromApi(string url)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode(); // Throw exception if response is not 2xx

                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching JSON: {ex.Message}");
                return "{}"; // Return empty JSON object instead of null
            }
        }
    }
}
