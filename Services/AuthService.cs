using Microsoft.Extensions.Options;
using SurveyBasket.API.ErrorsHandling;
using SurveyBasket.API.ErrorsHandling.CustomErrorMessages;
using SurveyBasket.API.Setting;
using System.Security.Cryptography;

namespace SurveyBasket.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly RefreshTokenOptions _refreshTokenOptions;
        private readonly JwtOptions _jwtOptions;

        public AuthService(
            UserManager<ApplicationUser> userManager,
            IMapper mapper,
            IOptions<JwtOptions> jwtOptions,
            IOptions<RefreshTokenOptions> RefreshTokenOptions)
        {
            _userManager = userManager;
            _mapper = mapper;
            _refreshTokenOptions = RefreshTokenOptions.Value;
            _jwtOptions = jwtOptions.Value;
        }

        public async Task<Result<LoginResponse>> LoginAsync(string Email, string Password, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(Email);
            if (user is not null)
            {
                var CheckPassword = await _userManager.CheckPasswordAsync(user, Password);
                if (CheckPassword)
                {
                    var CreatedToken = CreateToken(user, cancellationToken);
                    var RefreshToken = GenerateRefreshToken();
                    var RefreshTokenExpire = DateTime.UtcNow.AddDays(_refreshTokenOptions.ExpireInDays);
                    user.RefreshTokens.Add(new RefreshToken { Token = RefreshToken, ExpireOn = RefreshTokenExpire });

                    var loginResponse = new LoginResponse()
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        ValidToken = CreatedToken,
                        RefreshToken = RefreshToken,
                        RefreshTokenExpire = RefreshTokenExpire
                    };

                    await _userManager.UpdateAsync(user);
                    return  Result.Success(loginResponse);
                }
            }
            
            return Result.Failure<LoginResponse>(UserErrors.NotFoundUser);
        }
        public async Task<Result<LoginResponse>> GetRefreshToken(string token, string RefershToken, CancellationToken cancellationToken)
        {
            var userId = DecodingToken(token);
            if (userId is not null)
            {
                var GetUser = await _userManager.FindByIdAsync(userId);
                if (GetUser is not null)
                {
                    var GetUserRefreshToken = GetUser.RefreshTokens.FirstOrDefault(U => U.Token == RefershToken && U.IsActive);
                    if (GetUserRefreshToken is not null)
                        GetUserRefreshToken.RevokedAt = DateTime.UtcNow;

                    var NewToken = CreateToken(GetUser, cancellationToken);
                    var NewRefreshToken = GenerateRefreshToken();
                    var RefreshTokenExpire = DateTime.UtcNow.AddDays(_refreshTokenOptions.ExpireInDays);
                    GetUser.RefreshTokens.Add(new RefreshToken { Token = NewRefreshToken, ExpireOn = RefreshTokenExpire });

                    var loginResponse = new LoginResponse()
                    {
                        FirstName = GetUser.FirstName,
                        LastName = GetUser.LastName,
                        ValidToken = NewToken,
                        RefreshToken = NewRefreshToken,
                        RefreshTokenExpire = RefreshTokenExpire
                    };

                    await _userManager.UpdateAsync(GetUser);
                    return Result.Success(loginResponse);
                }

            }
            return Result.Failure<LoginResponse>(UserErrors.InvalidCredential);
        }

        public async Task<Result<LoginResponse>> RegisterAsync(RegistersRequest request, CancellationToken cancellationToken)
        {
            var user = request.Adapt<ApplicationUser>();
            var reuslt = await _userManager.CreateAsync(user,request.Password);
            if (reuslt.Succeeded)
            {
                var ValidToken = CreateToken(user, cancellationToken);
                var refreshToken= GenerateRefreshToken();
                var RegisterResponse = new LoginResponse()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    ValidToken = ValidToken,
                    RefreshToken = refreshToken,
                    RefreshTokenExpire = DateTime.UtcNow.AddDays(value: _refreshTokenOptions.ExpireInDays)
                };

                return Result.Success(RegisterResponse);
            }
            return Result.Failure<LoginResponse>(UserErrors.RegisertFailure);
        }
        public async Task<Result> RevokeRefreshToken(string token, string RefreshToken, CancellationToken cancellationToken)
        {
            var userId = DecodingToken(token);
            if (userId is not null)
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user is not null)
                {
                    var refreshToken = user.RefreshTokens.FirstOrDefault(x => x.Token == token && x.IsActive);
                    if (refreshToken is not null)
                    {
                        refreshToken.RevokedAt = DateTime.UtcNow;
                        await _userManager.UpdateAsync(user);
                        return Result.Success();
                    }
                }
            }
            return Result.Failure(UserErrors.InvalidOperation);

        }
        private static string GenerateRefreshToken()
        {
            var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
            return token;
        }


        private string? DecodingToken(string token)
        {
            var tokenhandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecurityKey));
            try
            {
                tokenhandler.ValidateToken(token, new TokenValidationParameters()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = jwtToken.Claims
                       .FirstOrDefault(j => j.Type == JwtRegisteredClaimNames.NameId);
                return userId?.Value;
            }
            catch
            {
                return null;
            }
        }

        private ValidToken CreateToken(ApplicationUser user, CancellationToken cancellationToken)
        {
            //Private Claims
            var Claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Jti,new Guid().ToString()),
                new Claim(JwtRegisteredClaimNames.GivenName,user.FirstName),
                new Claim(JwtRegisteredClaimNames.FamilyName,user.LastName),
                new Claim(JwtRegisteredClaimNames.NameId,user.Id),
                new Claim(JwtRegisteredClaimNames.Email,user.Email!),
            };
            //Secrete Key
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecurityKey));

            //Registerd Claims
            var Token = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,                                       //"https://localhost:7077",
                audience: _jwtOptions.Auidence,                                  //"https://localhost:4200",
                claims: Claims,
                signingCredentials: new SigningCredentials(Key, SecurityAlgorithms.HmacSha256),
                expires: DateTime.Now.AddMinutes(_jwtOptions.ExpireTokenIn)
                );


            var ReturnedToken = new JwtSecurityTokenHandler().WriteToken(Token).ToString();

            return new ValidToken(Token: ReturnedToken, ExpireIn: Token.ValidTo);


        }
    }
}
