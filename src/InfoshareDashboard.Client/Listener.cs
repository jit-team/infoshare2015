using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace InfoshareDashboard.Client
{
    class Listener
    {
        public readonly int Port;
        private readonly TcpListener _tcpListener;
        private readonly Task _listenTask;

        private readonly IHandler _handler;

        public Listener(IHandler handler, int port)
        {
            Port = port;

            // Start listening
            _tcpListener = new TcpListener(IPAddress.Any, Port);
            _tcpListener.Start();
            _handler = handler;
            
            Console.WriteLine($"Starting listening on {Port}");
            
            // Start a background thread to listen for incoming
            _listenTask = Task.Factory.StartNew(() => ListenLoop());
        }

        private async void ListenLoop()
        {
            for (; ;)
            {
                // Wait for connection
                var socket = await _tcpListener.AcceptSocketAsync();
                if (socket == null)
                    break;

                // Got new connection, create a handler for it
                var requestHandler = new RequestHandler(socket, _handler);
                // Create a task to handle new connection
                Task.Factory.StartNew(requestHandler.Handle);
            }
        }
    }
}