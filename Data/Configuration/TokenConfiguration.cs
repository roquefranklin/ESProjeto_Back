using ESProjeto_Back.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ESProjeto_Back.Data.Configuration
{
    public class TokenConfiguration : IEntityTypeConfiguration<Token>
    {
        public void Configure(EntityTypeBuilder<Token> builder)
        {
            builder
                .HasKey(token => token.Id);

            builder
                .HasOne(token => token.User)
                .WithMany(user => user.Tokens)
                .HasForeignKey(token => token.UserId)
                .IsRequired();

            builder
                .HasOne(token => token.RefreshToken)
                .WithOne()
                .HasForeignKey<Token>("OldRefreshToken")
                .IsRequired(false);

        }
    }
}
