using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;
using recipes_backend.Helpers;
using recipes_backend.Services.GoogleOAuthServiceModels;
using System.Net.Http;
using System.Text;
using System.Web;
using static System.Net.WebRequestMethods;

namespace recipes_backend.Services
{
    public class GoogleOAuthService
    {
        private const string ClientId = "192564777707-7onrl6ghb6nidm2hlofi3377cb7uj0c7.apps.googleusercontent.com";
        private const string ClientSecret = "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";

        private const string OAuthServerEndpoint = "https://accounts.google.com/o/oauth2/v2/auth";
        private const string TokenServerEndpoint = "https://oauth2.googleapis.com/token";

        private const string RedirectUrl = "https://localhost:5001/GoogleOAuth/Code";
        private const string emailScope = "https://www.googleapis.com/auth/userinfo.email";
        private const string profileScope = "https://www.googleapis.com/auth/userinfo.profile";
        private const string profileData = "https://www.googleapis.com/oauth2/v3/userinfo";



        public string GenerateOAuthRequestUrl()
        {
            var queryParams = new Dictionary<string, string>
            {
                {"client_id", ClientId },
                {"redirect_uri", RedirectUrl },
                {"response_type", "code" },
                {"scope", emailScope + " " + profileScope }
            };

            var url = QueryHelpers.AddQueryString(OAuthServerEndpoint, queryParams);
            return url;
        }

        public async Task<TokenResult> ExchangeCodeOnTokenAsync(string code)
        {
            var authParams = new Dictionary<string, string>
            {
                {"client_id", ClientId },
                {"client_secret", ClientSecret },
                {"code", code },
                {"grant_type", "authorization_code" },
                {"redirect_uri", RedirectUrl }
            };

            var tokenResult = await HttpClientHelper.SendPostRequest<TokenResult>(TokenServerEndpoint, authParams);
            return tokenResult;
        }

        public async Task<UserProfile> GetProfileData(string token)
        {
            UserProfile profile = await HttpClientHelper.SendGetRequest<UserProfile>(profileData, null, token);
            return profile;
        }
    }
}
