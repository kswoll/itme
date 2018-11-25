using System.Collections.Generic;
using System.Linq;
using ItMe.Models;
using Microsoft.AspNetCore.Http;

namespace ItMe.Utils
{
    public class AuthManager
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly HashSet<FeatureType> features = new HashSet<FeatureType>();

        public AuthManager(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;

            var user = httpContextAccessor.HttpContext.User;
            if (user.Identity.IsAuthenticated)
            {
                foreach (var featureClaim in user.Claims.Where(x => x.Type.StartsWith("feature:")))
                {
                    var feature = featureClaim.Type.Split(':')[1].Parse<FeatureType>();
                    features.Add(feature);
                }
            }
        }

        public bool IsFeatureEnabled(FeatureType feature)
        {
            return features.Contains(feature);
        }

        public bool IsLoggedIn => httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;
/*

        public void ProcessLogin(string token)
        {
            httpContextAccessor.HttpContext.Response.Cookies.Append(storageKey, token, new CookieOptions
            {
                Expires = new DateTimeOffset(DateTime.UtcNow.AddYears(5))
            });
            LoadToken(token);
            LoggedIn?.Invoke();
        }

        public void ProcessLogout()
        {
            httpContextAccessor.HttpContext.Response.Cookies.Delete(storageKey);
            Token = null;
            LoggedOut?.Invoke();
        }
*/
    }
}