using System.Text.Json;
using System.Threading.Tasks;
using BlazingPay.Abstractions.Contracts;
using BlazingPay.WebCommon;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.JSInterop;

namespace BlazingPay.Server.Services
{
    //We need to wait for .net 5 for IDataProtectionProvider in order to use this in wasm too i think
    public class JsInteropSecureConfigProvider : JsInteropConfigProvider, ISecureConfigProvider
    {
        private const string KeyPrefix = "encrypted:";
        private readonly IDataProtector _protector;

        public JsInteropSecureConfigProvider(IDataProtectionProvider dataProtectionProvider, IJSRuntime jsRuntime) : base(jsRuntime)
        {
            _protector = dataProtectionProvider.CreateProtector(nameof(JsInteropSecureConfigProvider));
        }

        public override async Task<T> Get<T>(string key)
        {
            var lsRes = await GetRaw($"{KeyPrefix}{key}");

            if (lsRes is null)
            {
                return default(T);
            }
            return JsonSerializer.Deserialize<T>(_protector.Unprotect(lsRes));
        }

        public override async Task Set<T>(string key, T value)
        {
            var defaultValue = default(T);
            
            if ((value is null && defaultValue is null) || (value?.Equals(default(T))??false) is true)
            {
                await base.Set($"{KeyPrefix}{key}", value);
            }
            else
            {
                await SetRaw($"{KeyPrefix}{key}", _protector.Protect(JsonSerializer.Serialize(value)));
            }
        }
    }
}