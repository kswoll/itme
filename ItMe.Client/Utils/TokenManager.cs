using System;
using System.Collections.Generic;
using System.Linq;
using Cloudcrate.AspNetCore.Blazor.Browser.Storage;
using ItMe.Shared;
using ItMe.Shared.Utils;

namespace ItMe.Client.Utils
{
	public class TokenManager
	{
		public JwtToken Token { get; private set; }
		public event Action LoggedOut;
		public event Action LoggedIn;

		private const string storageKey = "Token";

		private readonly LocalStorage storage;
        private readonly HashSet<FeatureType> features = new HashSet<FeatureType>();

		public TokenManager(LocalStorage storage)
		{
			this.storage = storage;

			var existingToken = storage[storageKey];
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
			storage[storageKey] = token;
			LoadToken(token);
			LoggedIn?.Invoke();
		}

		public void ProcessLogout()
		{
			storage.RemoveItem(storageKey);
			Token = null;
			LoggedOut?.Invoke();
		}
	}
}