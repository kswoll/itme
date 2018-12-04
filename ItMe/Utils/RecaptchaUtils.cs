using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ItMe.Utils
{
    public class RecaptchaUtils
    {
        public const string GoogleRecaptchaFormField = "g-recaptcha-response";

        private static readonly string GoogleRecaptchaSecret = Environment.GetEnvironmentVariable("ITME_RECAPTCHA_SECRET_KEY");

        public static async Task<bool> ValidateResponse(string response)
        {
            using (var client = new HttpClient())
            {
                var verifyResponse = await client.PostAsync($"https://www.google.com/recaptcha/api/siteverify?secret={GoogleRecaptchaSecret}&response={response}", new StringContent(""));
                var responseJson = await verifyResponse.Content.ReadAsStringAsync();
                var responseObject = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseJson);
                if (!(bool)responseObject["success"])
                {
                    return false;
                }
            }

            return true;
        }
    }
}