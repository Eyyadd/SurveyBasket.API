using SurveyBasket.API.ErrorsHandling.Exception_Handling;
using SurveyBasket.API.RegisterService;
using SurveyBasket.API.Setting;

namespace SurveyBasket.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.Services(builder.Configuration);
            builder.Services.AddExceptionHandler<ExceptionHandler>();
            builder.Services.AddProblemDetails();
            var app = builder.Build();

            var Scope = app.Services.CreateScope();
            var servic = Scope.ServiceProvider;
            var dbcontext = servic.GetRequiredService<SurveyBasketDbContext>();
            var logger = servic.GetRequiredService<ILoggerFactory>();
            var MigrationLogger = logger.CreateLogger<Program>();



            try
            {
                await dbcontext.Database.MigrateAsync();

            }
            catch (Exception ex)
            {
                MigrationLogger.LogError(ex.ToString());
            }
            finally
            {
                dbcontext.Dispose();
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.UseExceptionHandler();

            app.Run();
        }
    }
}
