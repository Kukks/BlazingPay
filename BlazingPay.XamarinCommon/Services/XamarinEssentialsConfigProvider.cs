using System.Text.Json;
using System.Threading.Tasks;
using BlazingPay.Abstractions.Contracts;
using Xamarin.Essentials;

namespace BlazingPay.XamarinCommon.Services
{
    public class XamarinEssentialsConfigProvider : IConfigProvider
    {
        public Task<T> Get<T>(string key)
        {
            return Task.FromResult(Preferences.ContainsKey(key)
                ? JsonSerializer.Deserialize<T>(Preferences.Get(key, ""))
                : default);
        }

        public Task Set<T>(string key, T value)
        {
            var defaultValue = default(T);
            
            if ((value is null && defaultValue is null) || (value?.Equals(default(T))??false) is true)
            {
                Preferences.Remove(key);
            }
            else
            {
                Preferences.Set(key, JsonSerializer.Serialize(value));
            }

            return Task.CompletedTask;
            ;
        }
    }
}