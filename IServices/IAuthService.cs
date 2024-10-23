namespace SurveyBasket.API.IServices
{
    public interface IAuthService
    {
        //ValidToken CreateToken(ApplicationUser user,CancellationToken cancellationToken);
        Task<LoginResponse?> LoginAsync(string Email,string Password,CancellationToken cancellationToken);
        Task<LoginResponse?> RegisterAsync(RegistersRequest request,CancellationToken cancellationToken);
        Task<LoginResponse?> GetRefreshToken(string token,string RefershToken,CancellationToken cancellationToken);
        Task<bool> RevokeRefreshToken(string token, string RefreshToken, CancellationToken cancellationToken);

    }
}
