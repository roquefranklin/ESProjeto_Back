using ESProjeto_Back.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ESProjeto_Back.Data.Configuration
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder
                .HasKey(review => review.Id);

            builder
                .HasOne(review => review.User)
                .WithMany(user => user.Reviews)
                .HasForeignKey(review => review.UserId)
                .IsRequired();

            builder
                .HasOne(review => review.StopPoint)
                .WithMany(stopPoint => stopPoint.Reviews)
                .HasForeignKey(review => review.StopPointId)
                .IsRequired();
        }
    }
}
