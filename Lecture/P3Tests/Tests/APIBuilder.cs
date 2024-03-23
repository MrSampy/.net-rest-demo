using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using P3;
using System.Net;

namespace P3Tests.Tests
{
    public class APIBuilder
    {
        private HttpClient _client;
        private WebApplicationFactory<Startup> _factory;

        public APIBuilder()
        {
            _factory = new WebApplicationFactory<Startup>();
            _client = _factory.CreateClient();
        }

        public async Task<T> GetRequest<T>(string endPoint)
        {
            var response = await _client.GetAsync(endPoint);

            string res = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(res)!;
        }

        public async Task<HttpResponseMessage> GetRequestReturnResponse(string endPoint)
        {
            var response = await _client.GetAsync(endPoint);

            return response;
        }

        public async Task<string> PostRequest(string endPoint, object value)
        {
            var json = JsonConvert.SerializeObject(value, new JsonSerializerSettings());
            var response = await _client.PostAsync(endPoint, new StringContent(json, null, "application/json"));

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> PutRequest(string endPoint, object value)
        {
            var json = JsonConvert.SerializeObject(value, new JsonSerializerSettings());
            var response = await _client.PutAsync(endPoint, new StringContent(json, null, "application/json"));

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<HttpResponseMessage> PuttRequestReturnResponse(string endPoint, object value)
        {
            var json = JsonConvert.SerializeObject(value, new JsonSerializerSettings());
            var response = await _client.PutAsync(endPoint, new StringContent(json, null, "application/json"));

            return response;
        }

        public async Task<string> DeleteRequest(string endPoint)
        {
            var response = await _client.DeleteAsync(endPoint);

            return await response.Content.ReadAsStringAsync();
        }


        public async Task<HttpResponseMessage> DeleteRequestReturnResponse(string endPoint)
        {
            var response = await _client.DeleteAsync(endPoint);

            return response;
        }
        public void Dispose()
        {
            _client.Dispose();
            _factory.Dispose();
        }

    }
}
