using System;
using System.Net.Http;
using Blazor.Extensions;
using Hara.Abstractions;
using Hara.Abstractions.Contracts;
using Hara.Abstractions.Services;
using Hara.Server.Services;
using Hara.UI.Services;
using Hara.WebCommon;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Hara.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();

            services.AddUIServices();
            services.AddSingleton<ICounterState, CounterState>();
            services.AddScoped<IWebsiteLauncher, JsInteropWebsiteLauncher>();
            services.AddSingleton<ILocalContentFetcher, FileProviderLocalContentFetcher>();
            services.AddSingleton<IWeatherForecastFetcher, WeatherForecastFetcher>();
            services.AddScoped<IConfigProvider, JsInteropConfigProvider>();
            services.AddScoped<ISecureConfigProvider, JsInteropSecureConfigProvider>();
            services.AddSingleton(provider =>
                provider.GetService<IWebHostEnvironment>().WebRootFileProvider);
            
            services.AddScoped<INotificationManager, WebNotificationManager>();
            services.AddDataProtection();
            services.AddNotifications();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}