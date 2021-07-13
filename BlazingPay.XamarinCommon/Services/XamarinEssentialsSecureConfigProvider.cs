using System.Text.Json;
using System.Threading.Tasks;
using BlazingPay.Abstractions.Contracts;
using Xamarin.Essentials;

namespace BlazingPay.XamarinCommon.Services
{
    public class XamarinEssentialsSecureConfigProvider : ISecureConfigProvider
    {
        public async Task<T> Get<T>(string key)
        {
            var raw = await SecureStorage.GetAsync(key);
            return string.IsNullOrEmpty(raw) ? default : JsonSerializer.Deserialize<T>(raw);
        }

        public async Task Set<T>(string key, T value)
        {
            var defaultValue = default(T);
            
            if ((value is null && defaultValue is null) || (value?.Equals(default(T))??false) is true)
            {
                SecureStorage.Remove(key);
            }
            else
            {
                await SecureStorage.SetAsync(key, JsonSerializer.Serialize(value));
            }
        }
    }
}