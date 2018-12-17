using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ItMe.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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

        public async Task ProcessLogin(string name, string email, int id)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, name),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.NameIdentifier, id.ToString())
            };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                IssuedUtc = DateTime.UtcNow,
                ExpiresUtc = DateTime.UtcNow.AddYears(10)
            };
            await httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
        }

        public async Task ProcessLogout()
        {
            await httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}