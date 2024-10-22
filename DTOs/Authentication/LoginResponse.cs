namespace SurveyBasket.API.DTOs.Authentication
{
    public class LoginResponse
    {
        public string FirstName {  get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        public ValidToken ValidToken { get; set; } = default!;
    }
}
