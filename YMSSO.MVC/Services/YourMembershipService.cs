using Microsoft.Extensions.Options;
using YMDiscourseSSO.Models;
using YMSDK;
using YMSDK.Providers;
using System.Linq;

namespace YMDiscourseSSO.Services
{
    public class YourMembershipService : IYourMembershipService
    {
        private readonly AppSettings appSettings;

        public YourMembershipService(IOptions<AppSettings> appSettings)
        {
            this.appSettings = appSettings.Value;
        }

        public YmTokenResult GetYmTokenResult(string username, string password)
        {
            var tokenResult = new YmTokenResult();

            var apiManager = this.CreateApiManager();
            apiManager.SessionCreate();
            try
            {
                var retUrl = "https://listserv.apf.org/";
                var result = apiManager.AuthCreateToken(retUrl, username, password, true);
                var auth = apiManager.AuthAuthenticate(username, password);

                var memberProfile = apiManager.MemberProfileGet();


                foreach (var item in result.MethodResults.Items)
                {
                    switch (item.Name)
                    {
                        case "AuthToken":
                            tokenResult.AuthToken = item.Value;
                            break;
                        case "GoToUrl":
                            tokenResult.GotoUrl = item.Value;
                            break;
                    }
                }
            } finally
            {
                apiManager.SessionAbandon();
            }

            return tokenResult;
        }

        private ApiManager CreateApiManager()
        {
            return new ApiManager(new XmlHttpProvider(this.appSettings.YmApiEndpoint))
            {
                Version = this.appSettings.YmApiVersion,
                ApiKeyPublic = this.appSettings.YmApiKeyPublic,
                ApiKeySa = this.appSettings.YmApiKeySA,
                SaPasscode = this.appSettings.YmApiSAPasscode,
                CallOrigin = this.appSettings.YmApiCallOrigin
            };
        }
    }
}
