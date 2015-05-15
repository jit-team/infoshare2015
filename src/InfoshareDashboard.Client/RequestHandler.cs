using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;

namespace InfoshareDashboard.Client
{
    class RequestHandler
    {
        private readonly Socket _socket;
        private readonly NetworkStream _networkStream;
        private readonly MemoryStream _memoryStream = new MemoryStream();
        private readonly Action<Request> _handler;
        private readonly StreamReader _streamReader;
        private readonly string _serverName = "Infoshare.AwesomeServer";

        public RequestHandler(Socket socket, Action<Request> handler)
        {
            _socket = socket;

            _handler = handler;
            _networkStream = new NetworkStream(socket, true);
            _streamReader = new StreamReader(_memoryStream);

        }

        private Request RequestOrigin()
        {

            var remoteIpEndPoint = _socket.RemoteEndPoint as IPEndPoint;
            var localIpEndPoint = _socket.LocalEndPoint as IPEndPoint;

            if (remoteIpEndPoint != null)
            {
                return new Request()
                {
                    Ip = remoteIpEndPoint.Address.ToString()
                };
            }
            if (localIpEndPoint != null)
            {
                return new Request()
                {
                    Ip = localIpEndPoint.Address.ToString()
                };
            }

            return null;
        }

        public async void Handle()
        {
            var bufferSize = 1024;

            byte[] bytes = null;
            var requestString = "";
            while (true)
            {
                bytes = new byte[bufferSize];
                int bytesRec = _socket.Receive(bytes);
                requestString += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                if (bytesRec < bufferSize)
                {
                    break;
                }
            }

            var requestLines = requestString.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);

            //handle actual HTTP request
            string requestData = requestLines[0];
            string[] tokens = requestData.Split(' ');
            if (tokens.Length != 3)
                return;

            string httpMethod = tokens[0].ToUpper();
            string httpUrl = tokens[1];

            Dictionary<string, string> headers = new Dictionary<string, string>();

            var startInd = 1;
            while (startInd < requestLines.Length && requestLines[startInd].Contains(":"))
            {
                var headerParts = requestLines[startInd].Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);

                if (string.IsNullOrEmpty(headerParts[0]) || string.IsNullOrEmpty(headerParts[1])) continue;

                headers.Add(headerParts[0].ToLower(), headerParts.Skip(1).Aggregate((s1, s2) => s1 + ":" + s2));

                startInd++;
            }
            string content = null;
            if (headers.ContainsKey("content-length"))
            {
                content = string.Join("\n", requestLines.Skip(startInd));
                if (Encoding.ASCII.GetByteCount(content) < int.Parse(headers["content-length"]))
                {
                    while (true)
                    {
                        bytes = new byte[bufferSize];
                        int bytesRec = _socket.Receive(bytes);
                        content += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                        if (bytesRec < bufferSize)
                        {
                            break;
                        }
                    }
                }
            }
            var request = RequestOrigin();
            request.Port = headers[Client.CLIENT_PORT_HEADER.ToLower()];
            
            try
            {
                var msg = JsonConvert.DeserializeObject<Models.Message>(content);
                request.Message = msg;

                Respond("200 OK");
            }
            catch (JsonReaderException jre)
            {
                Console.WriteLine(jre.Message);
                Respond("400 BAD REQUEST");
            }
            catch (ArgumentNullException ane)
            {
                Console.WriteLine(ane.Message);
                Respond("400 BAD REQUEST");
            }
            _handler?.Invoke(request);
        }


        private async void Respond(string responseCode)
        {
            string contentType = "";

            string header = string.Format("HTTP/1.1 {0}\r\n"
                                          + "Server: {1}\r\n"
                                          + "Content-Length: 0\r\n"
                                          + "Content-Type: {2}\r\n"
                                          + "Keep-Alive: Close\r\n"
                                          + "\r\n",
                                          responseCode, _serverName, contentType);
            // Send header & data
            var headerBytes = System.Text.Encoding.ASCII.GetBytes(header);
            await _networkStream.WriteAsync(headerBytes, 0, headerBytes.Length);
            await _networkStream.FlushAsync();
            // Close connection (we don't support keep-alive)
            _networkStream.Dispose();
        }

    }
}