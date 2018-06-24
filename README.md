# AzureADAuth-Unity

A sample Unity plugin to retrieve an auth token from Azure AD using UWP WebAccountManager API.

## How to use

1. Build the solution.
1. Import `AzureADAuth-Unity.Unity\bin\Release\AzureADAuthUnity.dll` and `AzureADAuth-Unity.UWP\bin\x86\Release\AzureADAuthUnity.dll` to your Unity project.
1. Check Editor/Standalone on the Inspector panel of the Unity dll, and check WSAPlayer on the panel for the UWP dll.
1. Call `authClient.GetAzureADToken()` from your Unity C# Script.
