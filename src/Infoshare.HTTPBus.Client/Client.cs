using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace InfoshareDashboard.Client
{
    public class Client
    {
        public static readonly string CLIENT_PORT_HEADER = "X-CLIENT-PORT";
        private HttpClient _client;
        private string _url;

        private Listener _listener;

        public Client(string serverUrl, int listenerPort, Action<Request> handler)
        {
            this._url = serverUrl;
            this._client = new HttpClient();
            this._listener = new Listener(handler, listenerPort);
            this._client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            this._client.DefaultRequestHeaders.Add(CLIENT_PORT_HEADER, listenerPort.ToString());
            
            Task.Run(CallIn);
        }

        public async Task<HttpStatusCode> Send(Models.Message message, string url = null)
        {
            var json = JsonConvert.SerializeObject(message);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(url == null ? _url : url, content);
            return response.StatusCode;
        }

        private async Task CallIn()
        {
            await _client.GetAsync(_url);
        }
    }
}
