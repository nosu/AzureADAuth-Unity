using AzureADAuthUnity.Shared;
using Windows.Security.Authentication.Web.Core;

namespace AzureADAuthUnity {
    public static class WebTokenResponseExtension {
        public static AzureADToken ConvertToAzureADToken(this WebTokenResponse response) {
            var token = new AzureADToken() {
                Id = response.WebAccount.Id,
                UserName = response.WebAccount.UserName,
                Token = response.Token
            };
            return token;
        }
    }
}
