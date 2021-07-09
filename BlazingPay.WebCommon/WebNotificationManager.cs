using System;
using System.Threading.Tasks;
using Blazor.Extensions;
using BlazingPay.Abstractions.Contracts;
using Microsoft.JSInterop;

namespace BlazingPay.WebCommon
{
    public class WebNotificationManager : INotificationManager
    {
        private const string AreSupportedFunctionName = "BlazorExtensions.Notifications.IsSupported";
        private const string RequestPermissionFunctionName = "BlazorExtensions.Notifications.RequestPermission";
        private const string CreateFunctionName = "BlazorExtensions.Notifications.Create";
        private readonly IJSRuntime _jsRuntime;

        public bool Initialized { get; private set; }
        public event EventHandler<NotificationEventArgs> NotificationReceived;

        public WebNotificationManager(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task<bool> Initialize()
        {
            Initialized = await IsSupportedByBrowserAsync() &&
                          await RequestPermissionAsync() == PermissionType.Granted;
            return Initialized;
        }

        public async Task<string> ScheduleNotification(string title, string message)
        {
            NotificationOptions options = new NotificationOptions
            {
                Body = message,
            };

            await _jsRuntime.InvokeAsync<object>(CreateFunctionName, title, options);
            return "";
        }

        public void ReceiveNotification(string title, string message)
        {
            NotificationReceived?.Invoke(null, new NotificationEventArgs()
            {
                Title = title,
                Message = message
            });
        }

        public ValueTask<bool> IsSupportedByBrowserAsync()
        {
            return _jsRuntime.InvokeAsync<bool>(AreSupportedFunctionName);
        }

        public async Task<PermissionType> RequestPermissionAsync()
        {
            string permission = await _jsRuntime.InvokeAsync<string>(RequestPermissionFunctionName);

            if (permission.Equals("granted", StringComparison.InvariantCultureIgnoreCase))
                return PermissionType.Granted;

            if (permission.Equals("denied", StringComparison.InvariantCultureIgnoreCase))
                return PermissionType.Denied;

            return PermissionType.Default;
        }
    }
}