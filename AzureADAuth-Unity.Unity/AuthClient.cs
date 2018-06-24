using AzureADAuthUnity.Shared;

namespace AzureADAuthUnity {
    public class AuthClient : IAuthClient {
        public AuthClient(string clientId, string resource) { }

        public void GetAzureADToken(AuthSuccessCallback onAuthCompleted, AuthFailedCallback onAuthFailed) {
            // ダミーの AzureADToken を返す
            onAuthCompleted(new AzureADToken() {
                Id = "Dummy ID",
                Token = "Dummy Token",
                UserName = "Dummy UserName"
            });
        }
    }
}
