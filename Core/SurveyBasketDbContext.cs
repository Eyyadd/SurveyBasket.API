using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SurveyBasket.API.Entities;

namespace SurveyBasket.API.Core
{
    public class SurveyBasketDbContext:IdentityDbContext<ApplicationUser>
    {
        public SurveyBasketDbContext(DbContextOptions<SurveyBasketDbContext> dbContextOptions):base(dbContextOptions) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Poll> Polls { get; set; }
    }
}
