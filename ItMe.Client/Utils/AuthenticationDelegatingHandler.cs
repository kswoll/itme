using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Blazor.Browser.Http;
using Microsoft.AspNetCore.Blazor.Services;

namespace ItMe.Client.Utils
{
    public class AuthenticationDelegatingHandler : DelegatingHandler
    {
        private readonly TokenManager tokenManager;
        private readonly IUriHelper uriHelper;

        public AuthenticationDelegatingHandler(IUriHelper uriHelper, TokenManager tokenManager) : base(new BrowserHttpMessageHandler())
        {
            this.uriHelper = uriHelper;
            this.tokenManager = tokenManager;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = tokenManager.Token?.Value;
            if (token != null) 
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await base.SendAsync(request, cancellationToken);
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                tokenManager.ProcessLogout();
                uriHelper.NavigateTo("login");
            }

            return response;
        }
    }
}