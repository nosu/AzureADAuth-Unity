using AzureADAuthUnity.Shared;
using System;
using Windows.Security.Authentication.Web.Core;
using Windows.Security.Credentials;
using Windows.UI.ApplicationSettings;

namespace AzureADAuthUnity {
    public class AuthClient : IAuthClient {
        private AuthSuccessCallback OnAuthSuccess;
        private AuthFailedCallback OnAuthFailed;

        private const string ProviderId = "https://login.microsoft.com";
        private const string Authority = "organizations";
        private const string AccountScopeRequested = "";
        private readonly string ClientId;
        private readonly string Resource;

        public AuthClient(string clientId, string resource) {
            this.ClientId = clientId;
            this.Resource = resource;
        }

        public void GetAzureADToken(AuthSuccessCallback onAuthCompleted, AuthFailedCallback onAuthFailed) {
            this.OnAuthSuccess = onAuthCompleted;
            this.OnAuthFailed = onAuthFailed;
            AccountsSettingsPane.GetForCurrentView().AccountCommandsRequested += OnAccountCommandsRequested;
            AccountsSettingsPane.Show();
        }

        private async void OnAccountCommandsRequested(AccountsSettingsPane sender, AccountsSettingsPaneCommandsRequestedEventArgs e) {
            AccountsSettingsPaneEventDeferral deferral = e.GetDeferral();

            WebAccountProvider provider = await WebAuthenticationCoreManager.FindAccountProviderAsync(ProviderId, Authority);
            WebAccountProviderCommand providerCommand = new WebAccountProviderCommand(provider, WebAccountProviderCommandInvoked);
            e.WebAccountProviderCommands.Add(providerCommand);

            deferral.Complete();
        }

        private async void WebAccountProviderCommandInvoked(WebAccountProviderCommand command) {
            try {
                WebTokenRequest webTokenRequest = new WebTokenRequest(command.WebAccountProvider, AccountScopeRequested, ClientId);
                webTokenRequest.Properties.Add("resource", Resource);

                WebTokenRequestResult webTokenRequestResult = await WebAuthenticationCoreManager.RequestTokenAsync(webTokenRequest);
                if(webTokenRequestResult.ResponseStatus == WebTokenRequestStatus.Success) {
                    OnAuthSuccess(webTokenRequestResult.ResponseData[0].ConvertToAzureADToken());
                } else {
                    OnAuthFailed($"Auth failed: {webTokenRequestResult.ResponseError.ErrorMessage}");
                }
            }
            catch(Exception ex) {
                OnAuthFailed($"Exception occurred: {ex.Message}");
            }
        }
    }
}
