using Microsoft.Extensions.Options;
using System;
using System.Text;
using System.Web;
using YMDiscourseSSO.Models;

namespace YMDiscourseSSO.Services
{
    public class DiscourseService : IDiscourseService
    {
        private readonly AppSettings appSettings;
        private readonly IHmacSHA256 hmacSHA256;

        public DiscourseService(IOptions<AppSettings> options,
            IHmacSHA256 hmacSHA256)
        {
            this.appSettings = options.Value;
            this.hmacSHA256 = hmacSHA256;
        }

        public string GetPlainNonce(string encodedNonce)
        {
            var decodedNonce = HttpUtility.UrlDecode(encodedNonce);
            var bytes = Convert.FromBase64String(decodedNonce);
            var rawPayload = Encoding.ASCII.GetString(bytes);
            var splits = rawPayload.Split("=");
            return splits[1];
        }

        public string GetRedirectUrl(DiscoursePayload discoursePayload)
        {
            var rawPayload = this.PrepareRawPayload(discoursePayload);
            var encodedPayload = this.Encode(rawPayload);
            var signedPayload = this.Sign(encodedPayload);
            return $"{this.appSettings.DiscourseSsoLoginUrl}?sso={encodedPayload}&sig={signedPayload}";
        }

        private string Sign(string message)
        {
            return this.hmacSHA256.CreateHexToken(message, this.appSettings.DiscourseSsoSecret);
        }

        private string Encode(string rawPayload)
        {
            var bytes = Encoding.ASCII.GetBytes(rawPayload);
            var base64String = Convert.ToBase64String(bytes);
            return HttpUtility.UrlEncode(base64String);
        }

        private string PrepareRawPayload(DiscoursePayload discoursePayload)
        {
            var payload = $"nonce={discoursePayload.Nonce}&email={discoursePayload.Email}";
            if (!string.IsNullOrWhiteSpace(discoursePayload.Name))
            {
                payload += $"&name={discoursePayload.Name}";
            }

            if (!string.IsNullOrWhiteSpace(discoursePayload.Surname))
            {
                payload += $"&surname={discoursePayload.Surname}";
            }

            if (!string.IsNullOrWhiteSpace(discoursePayload.ExternalId))
            {
                payload += $"&external_id={discoursePayload.ExternalId}";
            }

            if (discoursePayload.RequireActivation.HasValue)
            {
                payload += $"&require_activation={discoursePayload.RequireActivation.Value}";
            }

            return payload;
        }
    }
}
