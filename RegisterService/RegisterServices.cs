using Microsoft.Extensions.Options;
using SurveyBasket.API.ErrorsHandling.Exception_Handling;
using SurveyBasket.API.Setting;

namespace SurveyBasket.API.RegisterService
{
    public static class RegisterServices
    {
        public static IServiceCollection Services(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddControllers();
            services.SwagerServices();
            services.ApplcationService();
            services.FluentValidation();
            services.MapsterCongfig();
            services.AddDatabaseConfig(configuration);
           // services.Exceptions();
            return services;
        }

        public static IServiceCollection SwagerServices(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }
        public static IServiceCollection Exceptions(this IServiceCollection services)
        {
            services.AddExceptionHandler<ExceptionHandler>();
            services.AddProblemDetails();
            return services;
        }

        private static IServiceCollection ApplcationService(this IServiceCollection services)
        {
            services.AddScoped(typeof(IPollService), typeof(PollService));
            services.AddScoped(typeof(IAuthService), typeof(AuthService));
            services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));
            services.AddScoped(typeof(IPollsRepo), typeof(PollsRepo));
            return services;
        }
        private static IServiceCollection FluentValidation(this IServiceCollection services)
        {
            services
                .AddFluentValidationAutoValidation()
                .AddValidatorsFromAssemblyContaining<Program>();

            return services;
        }

        private static IServiceCollection MapsterCongfig(this IServiceCollection services)
        {
            var mappingConfig = TypeAdapterConfig.GlobalSettings;
            mappingConfig
                .Scan(Assembly.GetExecutingAssembly());
            services
                .AddSingleton<IMapper>(new Mapper(mappingConfig));
            return services;
        }
        private static IServiceCollection AddDatabaseConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtOptions>(configuration.GetSection("Jwt"));
            services.Configure<RefreshTokenOptions>(configuration.GetSection("RefreshToken"));
            var _jwtOptions = configuration.GetSection("Jwt").Get<JwtOptions>();

            #region Conection String & DataBase
            var ConnectionString = configuration
                    .GetConnectionString("ApplicationCs") ??
                    throw new InvalidOperationException("The Connection String {Application Cs} is not valid");

            services
                .AddDbContext<SurveyBasketDbContext>(options =>
                {
                    options.UseSqlServer(ConnectionString);
                });

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<SurveyBasketDbContext>(); 
            #endregion

            #region Authentication
            services.AddAuthentication(options =>
               {
                   options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                   options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
               }).AddJwtBearer(options =>
               {
                   options.SaveToken = true;
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuerSigningKey = true,
                       ValidateIssuer = true,
                       ValidIssuer = _jwtOptions?.Issuer,
                       ValidateAudience = true,
                       ValidAudience = _jwtOptions?.Auidence,
                       ValidateLifetime = true,
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions?.SecurityKey!))
                   };
               }); 
            #endregion


            return services;
        }
       
    }
}
