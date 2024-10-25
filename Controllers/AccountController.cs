
using SurveyBasket.API.ErrorsHandling;
using SurveyBasket.API.Helper.Extensions;

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
            var result = await _authService.LoginAsync(request.Email, request.Password, cancellationToken);
            return result.IsSucess ?
                Ok(result.Value) :
                result.Problems();
        }

        [HttpPost("auth/RefreshToken")]
        public async Task<IActionResult> RefreshTokenAsync(RefreshTokenRequest request, CancellationToken cancellationToken)
        {
            var result = await _authService.GetRefreshToken(request.Token, request.RefreshToken, cancellationToken);
            return result.IsSucess ?
            Ok(result.Value) :
           result.Problems();
        }

        [HttpPost("auth/RevokeRefreshToken")]
        public async Task<IActionResult> RevokeRefreshToken(RefreshTokenRequest request, CancellationToken cancellationToken)
        {
            var result = await _authService.RevokeRefreshToken(request.Token, request.RefreshToken, cancellationToken);
            return result.IsSucess ? Ok("Revoked") : result.Problems();
        }

        [HttpPost("auth/Register")]
        public async Task<IActionResult> Register(RegistersRequest request, CancellationToken cancellationToken)
        {
            var result = await _authService.RegisterAsync(request, cancellationToken);
            return result.IsSucess ? Ok(result.Value) :
                result.Problems();
        }
    }
}
