using SurveyBasket.API.Core;
using SurveyBasket.API.IRepositories;
using SurveyBasket.API.Repositories;

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
            return services;
        }

        public static IServiceCollection SwagerServices(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }

        public static IServiceCollection ApplcationService(this IServiceCollection services)
        {
            services.AddScoped(typeof(IPollService), typeof(PollService));
            services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));
            services.AddScoped(typeof(IPollsRepo), typeof(PollsRepo));

            return services;
        }
        public static IServiceCollection FluentValidation(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation().AddValidatorsFromAssemblyContaining<Program>();

            return services;
        }

        public static IServiceCollection MapsterCongfig(this IServiceCollection services)
        {
            var mappingConfig = TypeAdapterConfig.GlobalSettings;
            mappingConfig
                .Scan(Assembly.GetExecutingAssembly());
            services
                .AddSingleton<IMapper>(new Mapper(mappingConfig));
            return services;
        }
        public static IServiceCollection AddDatabaseConfig(this IServiceCollection services, IConfiguration configuration)
        {
            var ConnectionString = configuration
                .GetConnectionString("ApplicationCs") ?? 
                throw new InvalidOperationException("The Connection String {Application Cs} is not valid");

            services
                .AddDbContext<SurveyBasketDbContext>(options =>
                {
                    options.UseSqlServer(ConnectionString);
                });

            return services;
        }
    }
}
