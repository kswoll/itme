using System;
using System.Collections.Generic;
using System.Linq;
using ItMe.Models;
using Microsoft.AspNetCore.Http;

namespace ItMe.Utils
{
    public class TokenManager
    {
        public JwtToken Token { get; private set; }
        public event Action LoggedOut;
        public event Action LoggedIn;

        private const string storageKey = "Token";

        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly HashSet<FeatureType> features = new HashSet<FeatureType>();

        public TokenManager(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;

            var existingToken = httpContextAccessor.HttpContext.Request.Cookies[storageKey];
            if (existingToken != null)
            {
                LoadToken(existingToken);
            }
        }

        private void LoadToken(string token)
        {
            Token = JwtToken.Parse(token);
            foreach (var featureClaim in Token.Payload.Where(x => x.Key.StartsWith("feature:")))
            {
                var feature = featureClaim.Key.Split(':')[1].Parse<FeatureType>();
                features.Add(feature);
            }
        }

        public bool IsFeatureEnabled(FeatureType feature)
        {
            return features.Contains(feature);
        }

        public bool IsLoggedIn => Token != null;

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
    }
}