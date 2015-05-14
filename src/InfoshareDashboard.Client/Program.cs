namespace InfoshareDashboard.Client
{
    using System;
    using System.Threading.Tasks;
    public class Program
    {
        class MsgHandler : IHandler
        {
            public void Received(Models.Message message)
            {
                Console.WriteLine(message);
            }
        }

        static void Main(string[] args)
        {


            var client = new Client("http://localhost:8080", 8080, new MsgHandler());
            Task.Run(async () =>
            {
                await client.Send(new Models.Message()
                {
                    Content = "fdd"
                });
            });

        }
    }
}