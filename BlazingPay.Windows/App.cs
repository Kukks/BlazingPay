using Microsoft.MobileBlazorBindings.WebView.Windows;
using System;
using System.Threading.Tasks;
using BlazingPay.Abstractions.Contracts;
using BlazingPay.Server.Services;
using BlazingPay.WebCommon;
using BlazingPay.XamarinCommon;
using Microsoft.Extensions.DependencyInjection;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WPF;

namespace BlazingPay.Windows
{
    public class MainWindow : FormsApplicationPage
    {
        [STAThread]
        public static void Main()
        {
            var app = new System.Windows.Application();
            app.Run(new MainWindow());
        }

        public MainWindow()
        {
            Forms.Init();
            BlazorHybridWindows.Init();
            LoadApplication(new App(null,
                collection =>
                {
                    // collection.AddD
                    
                    collection.AddDataProtection();
                    collection.AddSingleton<INotificationManager, WebNotificationManager>();
                    collection.AddSingleton<IConfigProvider, JsInteropConfigProvider>();
                    collection.AddSingleton<ISecureConfigProvider, JsInteropSecureConfigProvider >();
                    // collection.AddSingleton<INotificationManager, StubNotificationManager>();}));
                }));
        }
    }
    
    public class StubNotificationManager: INotificationManager
    {
        public bool Initialized { get; } = false;
        public event EventHandler<NotificationEventArgs> NotificationReceived;
        public Task<bool> Initialize()
        {
            return Task.FromResult(false);
        }

        public Task<string> ScheduleNotification(string title, string message)
        {
            return Task.FromResult<string>(null);
        }

        public void ReceiveNotification(string title, string message)
        {
        }
    }
}
