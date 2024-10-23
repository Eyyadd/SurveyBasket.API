using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace SurveyBasket.API.Core
{
    public class SurveyBasketDbContext : IdentityDbContext<ApplicationUser>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SurveyBasketDbContext(DbContextOptions<SurveyBasketDbContext> dbContextOptions, IHttpContextAccessor httpContextAccessor) : base(dbContextOptions)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var Entires = ChangeTracker.Entries<AuditLogger>();
            foreach (var entire in Entires)
            {
                if (entire.State == EntityState.Added)
                {
                    var CurrentUser = _httpContextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
                    entire.Property(C => C.CreatedByUserId).CurrentValue = CurrentUser!;

                }
                else if (entire.State == EntityState.Modified)
                {
                    var CurrentUser = _httpContextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
                    entire.Property(C => C.UpdatedAt).CurrentValue = DateTime.UtcNow;
                    entire.Property(C => C.UpdatedByUserId).CurrentValue = CurrentUser!;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        public DbSet<Poll> Polls { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
    }
}
