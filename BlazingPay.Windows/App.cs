using Microsoft.MobileBlazorBindings.WebView.Windows;
using System;
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
                    collection.AddDataProtection();
                    collection.AddScoped<INotificationManager, WebNotificationManager>();
                    collection.AddScoped<IConfigProvider, JsInteropConfigProvider>();
                    collection.AddScoped<ISecureConfigProvider, JsInteropSecureConfigProvider >();
                    // collection.AddSingleton<INotificationManager, StubNotificationManager>();}));
                }));
        }
    }
}
