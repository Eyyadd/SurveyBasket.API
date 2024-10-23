namespace SurveyBasket.API.Validation
{
    public class RefreshTokenValidator:AbstractValidator<RefreshTokenRequest>
    {
        public RefreshTokenValidator()
        {
            RuleFor(RT=>RT.RefreshToken).NotEmpty();
            RuleFor(RT=>RT.Token).NotEmpty();
        }
    }
}
