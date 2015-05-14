﻿using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace InfoshareDashboard.Client
{
    public class Client
    {
        private HttpClient _client;
        private string _url;

        private Listener _listener;
        
        public Client(string url)
        {
            this._url = url;
            this._client = new HttpClient();
            this._listener = new Listener(null, 8080);
            this._client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<HttpStatusCode> Send(Models.Message message)
        {
            var json = JsonConvert.SerializeObject(message);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(_url, content);
            return response.StatusCode;           
        }
    }
}