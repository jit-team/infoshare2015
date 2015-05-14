using System;
using System.Net.Http;
using Microsoft.Net.Http.Client;

namespace InfoshareDashboard.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {

            // System.Diagnostics.Debugger.Launch();
            HttpClient client = new HttpClient(new ManagedHandler()
            {
                // ProxyAddress = new Uri("http://itgproxy:80")
            });
            var response = client.GetAsync(
            "http://wp.pl"
            ).Result;
            Console.WriteLine(response);
            // Console.WriteLine(response.Content.ReadAsStringAsync().Result);
        }
    }
}
