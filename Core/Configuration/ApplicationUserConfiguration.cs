﻿namespace SurveyBasket.API.Core.Configuration
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(U => U.FirstName)
                 .HasMaxLength(100);
            builder.Property(U => U.LastName)
                 .HasMaxLength(100);
            builder.OwnsMany(U => U.RefreshTokens)
                .WithOwner()
                .HasForeignKey("UserId");
        }
    }
}
