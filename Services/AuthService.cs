using Microsoft.Extensions.Options;
using SurveyBasket.API.Setting;

namespace SurveyBasket.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly JwtOptions _jwtOptions;

        public AuthService(UserManager<ApplicationUser> userManager, IMapper mapper, IOptions<JwtOptions> jwtOptions)
        {
            _userManager = userManager;
            _mapper = mapper;
            _jwtOptions = jwtOptions.Value;
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
                issuer: _jwtOptions.Issuer,                //"https://localhost:7077",
                audience:_jwtOptions.Auidence,                                  //"https://localhost:4200",
                claims: Claims,
                signingCredentials: new SigningCredentials(Key, SecurityAlgorithms.HmacSha256),
                expires: DateTime.UtcNow.AddMinutes(_jwtOptions.ExpireIn)
                );


            var ReturnedToken = new JwtSecurityTokenHandler().WriteToken(Token).ToString();

            return new ValidToken(Token: ReturnedToken, ExpireIn: Token.ValidFrom);


        }

        public async Task<LoginResponse?> LoginAsync(string Email, string Password, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(Email);
            if (user is not null)
            {
                var CheckPassword = await _userManager.CheckPasswordAsync(user, Password);
                if (CheckPassword)
                {
                    var CreatedToken = CreateToken(user, cancellationToken);
                    
                    var reps = new LoginResponse()
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        ValidToken = CreatedToken,
                    };
                    return reps;
                }
            }
            return null;
        }
    }
}
