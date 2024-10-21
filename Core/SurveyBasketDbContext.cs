namespace SurveyBasket.API.Core
{
    public class SurveyBasketDbContext:DbContext
    {
        public SurveyBasketDbContext(DbContextOptions<SurveyBasketDbContext> dbContextOptions):base(dbContextOptions) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<Poll> Polls { get; set; }
    }
}
