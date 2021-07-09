using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Hara.Abstractions;
using Hara.Abstractions.Contracts;

namespace Hara.WASM.Services
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
            var fileInfo = await _httpClient.GetAsync("_content/Hara.UI/weather.json");
            if (fileInfo.IsSuccessStatusCode)
            {
                return await fileInfo.Content.ReadAsStreamAsync();
            }

            return null;
        }
    }
}