using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfoshareDashboard.Hub
{
    public class Program
    {
        static HashSet<string> clients = new HashSet<string>();
        static Client.Client hub;
        public static void Main(string[] args)
        {
            hub = new Client.Client(null, 8081, Handle);
            Console.Read();
        }

        private static void Handle(Client.Request request)
        {
            var source = request.Ip.Trim() + ":" + request.Port.Trim();
            Console.WriteLine($"{source} sent message");
            clients.Add(source);
            Console.WriteLine(string.Join(",", clients));

            var broadcasts = clients
            .Where(address => address != source)
            .Select(address => Task.Run(async () =>
            {
                try
                {
                    await hub.Send(request.Message, $"http://{address}/");

                }
                catch (Exception we)
                {
                    Console.WriteLine($"Calling http://{address}/ resulted in {we.Message}");
                }
            }));
            Task.WaitAll(broadcasts.ToArray());
        }
    }
}
