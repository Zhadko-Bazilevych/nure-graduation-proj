using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using recipes_backend.Common;
using recipes_backend.Models;
using recipes_backend.Services;
using recipes_backend.Services.GoogleOAuthServiceModels;

namespace recipes_backend.Operations.OAuth.AuthByCode
{
    public class AuthByCodeOperation
    {
        recipesContext db;
        GoogleOAuthService oauthService;
        TokenService tokenService;
        private readonly IMapper _mapper;


        public AuthByCodeOperation(recipesContext db, GoogleOAuthService oauthService, TokenService tokenService, IMapper mapper)
        {
            this.db = db;
            this.oauthService = oauthService;
            this.tokenService = tokenService;
            this._mapper = mapper;
        }

        public async Task<AuthByCodeResponse> Execute(AuthByCodeRequest request)
        {
            var validate = await Validate(request);
            if (validate.Code != 200)
            {
                return new AuthByCodeResponse { Code = validate.Code, Message = validate.Message };
            }

            TokenResult tokenResult = await oauthService.ExchangeCodeOnTokenAsync(request.Code);

            UserProfile profile = await oauthService.GetProfileData(tokenResult.AccessToken);

            var user = await db.Users.Include(y=>y.RefreshTokens).Where(x => x.Mail == profile.Mail).FirstOrDefaultAsync();

            if (user == null)
            {
                User newUser = _mapper.Map<User>(profile);
                db.Users.Add(newUser);
                db.SaveChanges();
                int newUserId = (await db.Users.Where(x => x.Mail == profile.Mail).FirstAsync()).Id;
                string newRefresh = tokenService.CreateToken(newUser, true);
                db.RefreshTokens.Add(new RefreshToken()
                {
                    UserId = newUserId,
                    Token = newRefresh,
                    Device = request.Device,
                });
                db.SaveChanges();
                AuthByCodeResponse result = new AuthByCodeResponse()
                {
                    Mail = newUser.Mail,
                    Name = newUser.Name,
                    AccessToken = tokenService.CreateToken(newUser),
                    RefreshToken = newRefresh,
                };
                return result;
            }
            else
            {
                RefreshToken? RT = user.RefreshTokens.Where(x=>x.Device == request.Device).FirstOrDefault();
                var newAccess = tokenService.CreateToken(user);
                var newRefresh = tokenService.CreateToken(user, true);

                if (RT==null)
                {
                    db.RefreshTokens.Add(new RefreshToken()
                    {
                        Device = request.Device,
                        UserId = user.Id,
                        Token = newRefresh,
                    });
                }
                else
                {
                    (await db.RefreshTokens.Where(x=>x.Id == RT.Id).FirstAsync()).Token = newRefresh;
                }
                db.SaveChanges();

                return new AuthByCodeResponse()
                {
                    Mail = user.Mail,
                    Name = user.Name,
                    AccessToken = newAccess,
                    RefreshToken = newRefresh,
                };
            }
        }

        public async Task<ValidateResult> Validate(AuthByCodeRequest request)
        {
            return new ValidateResult();
        }
    }
}
