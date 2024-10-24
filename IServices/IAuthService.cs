using SurveyBasket.API.ErrorsHandling;

namespace SurveyBasket.API.IServices
{
    public interface IAuthService
    {
        //ValidToken CreateToken(ApplicationUser user,CancellationToken cancellationToken);
        Task<Result<LoginResponse>> LoginAsync(string Email,string Password,CancellationToken cancellationToken);
        Task<Result<LoginResponse>> RegisterAsync(RegistersRequest request,CancellationToken cancellationToken);
        Task<Result<LoginResponse>> GetRefreshToken(string token,string RefershToken,CancellationToken cancellationToken);
        Task<Result> RevokeRefreshToken(string token, string RefreshToken, CancellationToken cancellationToken);

    }
}
