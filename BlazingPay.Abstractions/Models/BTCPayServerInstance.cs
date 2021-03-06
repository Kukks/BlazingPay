using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using BlazingPay.Abstractions.Contracts;
using BlazingPay.Abstractions.Services;
using BlazingPay.UI.Services;
using BTCPayServer.Client;

namespace BlazingPay.Abstractions.Models
{

    public class InstanceRepository
    {

        public event Action StateChanged;
        private Dictionary<string, string> InstanceList;
        private Dictionary<string, BTCPayServerInstance> Instances = new Dictionary<string, BTCPayServerInstance>();
        private readonly IConfigProvider _configProvider;
        private readonly ISecureConfigProvider _secureConfigProvider;

        public InstanceRepository(IConfigProvider configProvider, ISecureConfigProvider secureConfigProvider)
        {
            _configProvider = configProvider;
            _secureConfigProvider = secureConfigProvider;
        }

        public async Task<Dictionary<string, string>> GetList(bool refresh = false)
        {
            if (InstanceList is null || refresh)
            {
                InstanceList = (await _configProvider.Get<Dictionary<string, string>>("InstancesList")) ?? new Dictionary<string, string>();
                StateChanged?.Invoke();
                
            }

            return InstanceList;
        }

        public async Task<BTCPayServerInstance> Get(string id)
        {
            if (Instances.TryGetValue(id, out var result))
            {
                return result;
            }

            result = await _secureConfigProvider.Get<BTCPayServerInstance>($"Instance_{id}");
            if (result is null)
            {
                return result;
            }
            Instances.TryAdd(id,result );
            
            return result;
        }

        public async Task<string> Set(BTCPayServerInstance instance)
        {
            var id =instance.GetId();
            var list = await GetList();
            if (!list.TryGetValue(id, out var existingValue))
            {
                list.Add(id, instance.Label);
                await _configProvider.Set("InstancesList", list);
            }else if (existingValue != instance.Label)
            {
                list[id] = instance.Label;
                await _configProvider.Set("InstancesList", list);
            }

            await _secureConfigProvider.Set($"Instance_{id}", instance);
            StateChanged?.Invoke();
            return id;
        }
    }

    public class BTCPayServerInstance
    {
        public string Label { get; set; }
        public Uri  Url { get; set; }
        public string ApiKey { get; set; }
        [JsonConverter(typeof(ListJsonConverterConverter<Permission, PermissionJsonConverter>))]
        public List<Permission> Permissions { get; set; }

        public string GetId()
        {
            return HashUtils.GetHashString(Url + ApiKey);
        }

        public BTCPayServerClient Construct()
        {
            return new BTCPayServerClient(Url, ApiKey);
        }
    }
    
    
}