using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.JSInterop;

namespace ItMe.Client.Utils
{
	public class JwtToken
	{
		public string Value { get; private set; }
		public string Alg { get; private set; }
		public string Typ { get; private set; }
		public IReadOnlyDictionary<string, string> Payload { get; private set; }

		public int Id { get; private set; }
		public string Email { get; set; }
		public string Name { get; set; }

		public static JwtToken Parse(string s)
		{
			var parts = s.Split('.');
			parts = parts.Take(2).Select(x => Encoding.UTF8.GetString(Convert.FromBase64String(x.PadRight(x.Length + (4 - x.Length % 4) % 4, '=')))).ToArray();
			var part1 = Json.Deserialize<Dictionary<string, string>>(parts[0]);
			var payload = Json.Deserialize<Dictionary<string, string>>(parts[1]);
			
			var id = int.Parse(payload["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"]);
			var email = payload["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"];
			var name = payload["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"];

			return new JwtToken
			{
				Value = s,
				Alg = part1["alg"],
				Typ = part1["typ"],
				Payload = payload,
				Id = id,
				Name = name,
				Email = email
			};
		}
	}
}