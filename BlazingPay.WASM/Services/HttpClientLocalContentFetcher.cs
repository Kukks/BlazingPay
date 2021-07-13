using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using BlazingPay.Abstractions;
using BlazingPay.Abstractions.Contracts;

namespace BlazingPay.WASM.Services
{
    public class HttpClientLocalContentFetcher : ILocalContentFetcher
    {
        private readonly HttpClient _httpClient;

        public HttpClientLocalContentFetcher(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Stream> Fetch(string path)
        {
            var fileInfo = await _httpClient.GetAsync(path);
            if (fileInfo.IsSuccessStatusCode)
            {
                return await fileInfo.Content.ReadAsStreamAsync();
            }

            return null;
        }
    }
}