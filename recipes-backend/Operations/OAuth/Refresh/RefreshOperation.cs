using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using recipes_backend.Common;
using recipes_backend.Models;
using recipes_backend.Operations.OAuth.Refresh;
using recipes_backend.Services;

namespace recipes_backend.Operations.OAuth.Refresh
{
    public class RefreshOperation
    {
        recipesContext db;
        GoogleOAuthService oauthService;
        TokenService tokenService;

        public RefreshOperation(recipesContext db, GoogleOAuthService oauthService, TokenService tokenService)
        {
            this.db = db;
            this.oauthService = oauthService;
            this.tokenService = tokenService;
        }
        public async Task<RefreshResponse> Execute(RefreshRequest request)
        {
            var validate = await Validate(request);
            if (validate.Code != 200)
            {
                return new RefreshResponse { Code = validate.Code, Message = validate.Message };
            }

            if (db.RefreshTokens.Any(x=>x.Device==request.Device && x.Token==request.RefreshToken))
            {
                var refreshToken = await db.RefreshTokens.Where(x => x.Device == request.Device && x.Token == request.RefreshToken).FirstAsync();
                var tokenUser = await db.Users.Where(x => x.Id == refreshToken.UserId).FirstAsync();
                var newAccessToken = tokenService.CreateToken(tokenUser);
                var newRefreshToken = tokenService.CreateToken(tokenUser, true);

                refreshToken.Token = newRefreshToken;
                db.SaveChanges();
                RefreshResponse result = new RefreshResponse()
                {
                    Mail = tokenUser.Mail,
                    Name = tokenUser.Name,
                    AccessToken = newAccessToken,
                    RefreshToken = newRefreshToken,
                };
                return result;
            }
            else
            {
                return new RefreshResponse { Code = 401, Message = "Invalid credentials" };
            }
        }

        public async Task<ValidateResult> Validate(RefreshRequest request)
        {
            return new ValidateResult();
        }
    }
}
