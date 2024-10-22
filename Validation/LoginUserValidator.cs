namespace SurveyBasket.API.Validation
{
    public class LoginUserValidator:AbstractValidator<LoginRequest>
    {
        public LoginUserValidator()
        {
            RuleFor(U => U.Email)
                .NotEmpty()
                .EmailAddress();
            RuleFor(U => U.Password)
                .NotEmpty();
        }
    }
}
