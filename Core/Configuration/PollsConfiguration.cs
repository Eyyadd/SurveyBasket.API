using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SurveyBasket.API.Core.Configuration
{
    public class PollsConfiguration : IEntityTypeConfiguration<Poll>
    {
        public void Configure(EntityTypeBuilder<Poll> builder)
        {
            builder
                .HasIndex(P => P.Title)
                .IsUnique();
            builder
                .Property(P => P.Title)
                .HasMaxLength(100);
            builder
                .Property(P=>P.Summary)
                .HasMaxLength (1500);
        }
    }
}
