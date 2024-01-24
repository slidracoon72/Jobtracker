using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class GoogleTokenValidationAttribute : Attribute, IAsyncAuthorizationFilter
{
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        string? accessToken = GetAccessToken(context.HttpContext.Request);

        if (string.IsNullOrEmpty(accessToken))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        bool isTokenValid;

        // Check if the token is already verified in the session
        if (context.HttpContext.Session.TryGetValue($"VerifiedToken_{accessToken}", out _))
        {
            // Token is already verified, skip verification
            isTokenValid = true;
        }
        else
        {
            // Verify the Google Access Token
            isTokenValid = await VerifyGoogleToken(accessToken);
            if(isTokenValid)
                context.HttpContext.Session.Set($"VerifiedToken_{accessToken}", new byte[1]);

            // Cache the verification result in the session
        }

        if (!isTokenValid)
        {
            context.Result = new UnauthorizedResult();
        }
    }

    private string? GetAccessToken(HttpRequest request)
    {
        return request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
    }

    private async Task<bool> VerifyGoogleToken(string accessToken)
    {
        try
        {
            FirebaseApp firebaseApp = FirebaseApp.DefaultInstance;

            if (firebaseApp == null)
            {
                firebaseApp = FirebaseApp.Create(new AppOptions
                {
                    Credential = GoogleCredential.FromFile(Path.Combine(Directory.GetCurrentDirectory(), "Firebase", "credentials.json")),
                });
            }

            FirebaseToken? decodedToken = await FirebaseAdmin.Auth.FirebaseAuth.DefaultInstance
                .VerifyIdTokenAsync(accessToken);

            // Token is valid
            return true;
        }
        catch (Exception ex)
        {
            // Token verification failed
            Console.WriteLine($"Firebase token verification failed: {ex.Message}");
            return false;
        }
    }



}
