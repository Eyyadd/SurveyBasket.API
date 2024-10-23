namespace SurveyBasket.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("auth/Login")]
        public async Task<IActionResult> Login(LoginRequest request, CancellationToken cancellationToken)
        {
            var CanLogin = await _authService.LoginAsync(request.Email, request.Password, cancellationToken);
            if (CanLogin is not null)
            {
                return Ok(CanLogin);
            }
            return BadRequest("invalid login");
        }

        [HttpPost("auth/RefreshToken")]
        public async Task<IActionResult> RefreshTokenAsync(RefreshTokenRequest request,CancellationToken cancellationToken)
        {
            var GetRefreshToken= await _authService.GetRefreshToken(request.Token, request.RefreshToken, cancellationToken);
            if (GetRefreshToken is not null)
            {
                return Ok(GetRefreshToken);
            }
            return BadRequest("Invalid operation");
        }
    }
}
