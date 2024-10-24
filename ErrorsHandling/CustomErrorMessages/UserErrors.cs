namespace SurveyBasket.API.ErrorsHandling.CustomErrorMessages
{
    public static class UserErrors
    {
        public static readonly Error NotFoundUser = new ("404", "User Not Found");
        public static readonly Error RegisertFailure = new ("Invalid Registeration", "sorry you can not register");
        public static readonly Error InvalidOperation = new ("Invalid Operation", "sorry we can not do this operation");
        public static readonly Error InvalidCredential = new ("InvalidCredentialLogin", "Token / RefreshToken is invalid");
    }
}
