using Client.Infrastructure.Services;
using Client.ViewModels;
using Client.ViewModels.Profile;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await Task.Delay(5000);
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddMudServices();

            builder.Services.AddScoped<IProfileService, ProfileService>();

            builder.Services.AddTransient<MainViewModel>();

            builder.Services.AddTransient<ProfileViewModel>();
            builder.Services.AddTransient<FirstTabViewModel>();
            builder.Services.AddTransient<SecondTabViewModel>();

            await builder.Build().RunAsync();
        }
    }
}