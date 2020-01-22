using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var baseAddress = new Uri("https://localhost:44316/");

            {// Request に Version を付けるパターン
                var client = new HttpClient() { BaseAddress = baseAddress };

                // HTTP/1.1 request
                using (var response = await client.GetAsync("/weatherforecast?message=request1.1"))
                    Console.WriteLine(response.Content);

                // HTTP/2 request
                using (var request = new HttpRequestMessage(HttpMethod.Get, "/weatherforecast?message=request2.0") { Version = new Version(2, 0) })
                using (var response = await client.SendAsync(request))
                    Console.WriteLine(response.Content);
            }

            {// HttpClient に Version を付けるパターン
                {
                    var client = new HttpClient()
                    {
                        BaseAddress = baseAddress
                    };

                    // HTTP/1.1 is default
                    using (var response = await client.GetAsync("/weatherforecast?message=HttpClient1.1"))
                        Console.WriteLine(response.Content);
                }
                {
                    var client = new HttpClient()
                    {
                        BaseAddress = baseAddress,
                        DefaultRequestVersion = new Version(2, 0)
                    };

                    // HTTP/2 is default
                    using (var response = await client.GetAsync("/weatherforecast?message=HttpClient2.0"))
                        Console.WriteLine(response.Content);
                }
            }
        }
    }
}
