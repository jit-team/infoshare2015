using System;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.Net.Http.Client;
using Newtonsoft.Json;

namespace InfoshareDashboard.Client
{
    public class Program
    {
        public static void Main(string[] args)
        {

            // System.Diagnostics.Debugger.Launch();
            HttpClient client = new HttpClient();
            
            Task.Run(async () =>
            {
                var msg = new InfoshareDashboard.Models.Message(){
                    Content = "Hej",
                    Sendee = "Lukasz"                   
                };
                var content = new StringContent(JsonConvert.SerializeObject(msg));
                
                var response = await client.PostAsync("http://localhost:5004/messages/push", content);
                Console.WriteLine(response.StatusCode);
            }).Wait();

            Console.Read();
            // Console.WriteLine(response.Content.ReadAsStringAsync().Result);
        }
    }
}
