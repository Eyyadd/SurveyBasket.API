using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurveyBasket.API.DTOs.Authentication;

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
            var Logged = await _authService.LoginAsync(request.Email, request.Password, cancellationToken);
            if (Logged != null)
            {
                return Ok(Logged);
            }
            return BadRequest("invalid login");
        }
    }
}
