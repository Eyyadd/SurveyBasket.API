namespace SurveyBasket.API.MappingMapster.AuthConfig
{
    public class ApplicationUser : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<LoginResponse, ApplicationUser>()
                 .TwoWays();
        }
    }
}
