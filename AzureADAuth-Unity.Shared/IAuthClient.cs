using System;
using System.Collections.Generic;
using System.Text;

namespace AzureADAuthUnity.Shared {
    public delegate void AuthSuccessCallback(AzureADToken token);
    public delegate void AuthFailedCallback(string errorMessage);

    public interface IAuthClient {
        void GetAzureADToken(AuthSuccessCallback onAuthCompleted, AuthFailedCallback onAuthFailed);
    }
}
