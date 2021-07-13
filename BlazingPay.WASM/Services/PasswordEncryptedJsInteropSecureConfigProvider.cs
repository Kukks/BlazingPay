using System.Text.Json;
using System.Threading.Tasks;
using BlazingPay.Abstractions.Contracts;
using BlazingPay.Abstractions.Services;
using BlazingPay.WebCommon;
using Microsoft.JSInterop;

namespace BlazingPay.WASM.Services
{
    public class PasswordEncryptedJsInteropSecureConfigProvider : JsInteropConfigProvider, ISecureConfigProvider
    {
        private const string KeyPrefix = "encrypted:";
        private readonly IJSRuntime _jsRuntime;
        public string Password { get; set; }

        public PasswordEncryptedJsInteropSecureConfigProvider(IJSRuntime jsRuntime) : base(jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        private async Task AskForPassword(int count = 0)
        {
            if (string.IsNullOrEmpty(Password))
            {
                var res = await _jsRuntime.InvokeAsync<string>("prompt",
                    "Enter passphrase to decrypt secure web storage.");
                if (!string.IsNullOrEmpty(res))
                {
                    var lsRes = await GetRaw($"{KeyPrefix}passwordtest");
                    if (string.IsNullOrEmpty(lsRes))
                    {
                        Password = res;
                        await Set($"passwordtest", "passwordtest");
                        var decryptTest = await Get<string>("passwordtest");
                        await _jsRuntime.InvokeVoidAsync("alert",
                            $"saved key test and decoded it correctly: {(decryptTest == "passwordtest")}");
                    }
                    else if (DataEncryptor.Decrypt(lsRes, res) != "\"passwordtest\"")
                    {
                        await _jsRuntime.InvokeVoidAsync("alert", $"Invalid password.");
                        await AskForPassword(count+1);
                    }
                    else
                    {
                        Password = res;
                    }
                }
            }
        }

        public override async Task<T> Get<T>(string key)
        {
            await AskForPassword();
            var lsRes = await GetRaw($"{KeyPrefix}{key}");

            if (lsRes is null)
            {
                return default(T);
            }

            var decrypted = DataEncryptor.Decrypt(lsRes, Password);
            return JsonSerializer.Deserialize<T>(decrypted);
        }

        public override async Task Set<T>(string key, T value)
        {
            await AskForPassword();
            var defaultValue = default(T);
            
            if ((value is null && defaultValue is null) || (value?.Equals(default(T))??false) is true)
            {
                await base.Set($"{KeyPrefix}{key}", value);
            }
            else
            {
                await SetRaw($"{KeyPrefix}{key}", DataEncryptor.Encrypt(JsonSerializer.Serialize(value), Password));
            }
        }
    }
}