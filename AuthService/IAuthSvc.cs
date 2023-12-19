using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Apis.Auth;
using ModelService;

namespace AuthService
{
    public interface IAuthSvc
    {

        Task<TokenResponseModel> Auth(LoginViewModel model);
        Task<TokenResponseModel> Auth(TokenRequestModel model);
        Task<TokenResponseModel> GenerateNewToken();
        // new token when we come from google
        Task<TokenResponseModel> GenerateNewToken(ApplicationUser user);
        Task<bool> LogoutUserAsync();
        void DeleteAllCookies(IEnumerable<string> cookiesToDelete);
        Task<GoogleJsonWebSignature.Payload> VerifyGoogleToken(ExternalAuthModel externalAuth);
        void DeleteCookie(string name);
    }
}
