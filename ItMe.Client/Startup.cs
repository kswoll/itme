using System;
using System.Net.Http;
using Cloudcrate.AspNetCore.Blazor.Browser.Storage;
using ItMe.Client.Utils;
using Microsoft.AspNetCore.Blazor.Browser.Services;
using Microsoft.AspNetCore.Blazor.Builder;
using Microsoft.AspNetCore.Blazor.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ItMe.Client
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
			services.AddStorage();
			services.AddSingleton(x => new HttpClient(new AuthenticationDelegatingHandler(x.GetService<IUriHelper>(), x.GetService<TokenManager>()))
			{
				BaseAddress = new Uri(BrowserUriHelper.Instance.GetBaseUri())
			});
			services.AddSingleton(x => new TokenManager(x.GetService<LocalStorage>()));
        }

        public void Configure(IBlazorApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
