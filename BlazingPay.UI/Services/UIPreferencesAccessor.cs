using System;
using System.Threading.Tasks;
using BlazingPay.Abstractions.Contracts;

namespace BlazingPay.UI.Services
{
    public class UIPreferencesAccessor
    {
        public event Action StateChanged;
        private readonly IConfigProvider _configProvider;
        private readonly ThemeSwitcher _themeSwitcher;
        private static UIPreferences _uiPreferences;

        public UIPreferencesAccessor(IConfigProvider configProvider, ThemeSwitcher themeSwitcher)
        {
            _configProvider = configProvider;
            _themeSwitcher = themeSwitcher;
        }

        public async Task<UIPreferences> Get()
        {
            _uiPreferences ??= (await _configProvider.Get<UIPreferences>(nameof(UIPreferences)))??new UIPreferences();
            return _uiPreferences;
        }
        
        public async Task Set(UIPreferences preferences)
        {
            await _themeSwitcher.ToggleDark(preferences.DarkMode.GetValueOrDefault(true));

            _uiPreferences = preferences;
            await _configProvider.Set(nameof(UIPreferences), preferences);
            StateChanged?.Invoke();
        }

    }
}